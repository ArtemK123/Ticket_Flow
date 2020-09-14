using System.Collections.Generic;
using System.Linq;
using TicketFlow.Common.Providers;

namespace TicketFlow.Common.Repositories
{
    public abstract class MappedCrudRepositoryBase<TIdentifier, TEntity, TEntityDatabaseModel>
        : CrudRepositoryBase<TIdentifier, TEntity, TEntityDatabaseModel>
    {
        protected MappedCrudRepositoryBase(IDbConnectionProvider dbConnectionProvider)
            : base(dbConnectionProvider)
        {
        }

        protected override string SelectByIdentifierQuery => $"SELECT {GetSelectSqlMapping()} FROM {TableName} WHERE {GetPrimaryKeyConditionMapping()};";

        protected override string SelectAllQuery => $"SELECT {GetSelectSqlMapping()} FROM {TableName};";

        protected override string InsertQuery => $"INSERT INTO {TableName} ({GetInsertColumnsList()}) VALUES ({GetInsertFieldsList()});";

        protected override string UpdateQuery => $"UPDATE {TableName} SET {GetUpdateSqlMapping()} WHERE {GetPrimaryKeyConditionMapping()};";

        protected override string DeleteQuery => $"DELETE FROM {TableName} WHERE {GetPrimaryKeyConditionMapping()};";

        protected abstract string TableName { get; }

        protected abstract KeyValuePair<string, string> PrimaryKeyMapping { get; }

        protected abstract IReadOnlyDictionary<string, string> NonPrimaryColumnsMapping { get; }

        protected string GetSelectSqlMapping()
            => string.Join(
                ", ",
                new List<string> { GetSelectSqlMemberMapping(PrimaryKeyMapping) }
                    .Concat(NonPrimaryColumnsMapping.Select(GetSelectSqlMemberMapping)));

        private static string GetSelectSqlMemberMapping(KeyValuePair<string, string> columnFieldMapping) => $"{columnFieldMapping.Key} AS {columnFieldMapping.Value}";

        private static string GetUpdateSqlMemberMapping(KeyValuePair<string, string> columnFieldMapping) => $"{columnFieldMapping.Key}=@{columnFieldMapping.Value}";

        private string GetPrimaryKeyConditionMapping() => $"{PrimaryKeyMapping.Key}=@{PrimaryKeyMapping.Value}";

        private string GetInsertColumnsList()
            => string.Join(
                ", ",
                new List<string> { PrimaryKeyMapping.Key }
                    .Concat(NonPrimaryColumnsMapping.Select(columnFieldPair => columnFieldPair.Key)));

        private string GetInsertFieldsList()
            => string.Join(
                ", ",
                new List<string> { $"@{PrimaryKeyMapping.Value}" }
                    .Concat(NonPrimaryColumnsMapping.Select(columnFieldPair => $"@{columnFieldPair.Value}")));

        private string GetUpdateSqlMapping() => string.Join(", ", NonPrimaryColumnsMapping.Select(GetUpdateSqlMemberMapping));
    }
}