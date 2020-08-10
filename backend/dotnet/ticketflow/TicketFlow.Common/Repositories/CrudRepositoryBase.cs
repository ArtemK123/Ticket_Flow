using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;

namespace TicketFlow.Common.Repositories
{
    public abstract class CrudRepositoryBase<TIdentifier, TEntity, TEntityDatabaseModel> : ICrudRepository<TIdentifier, TEntity>
    {
        protected CrudRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            DbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string SelectByIdentifierQuery { get; }

        protected abstract string SelectAllQuery { get; }

        protected abstract string InsertQuery { get; }

        protected abstract string UpdateQuery { get; }

        protected abstract string DeleteQuery { get; }

        protected IDbConnectionProvider DbConnectionProvider { get; }

        public bool TryGet(TIdentifier identifier, out TEntity entity)
        {
            entity = default;
            using var dbConnection = DbConnectionProvider.Get();
            TEntityDatabaseModel databaseModel = dbConnection.Query<TEntityDatabaseModel>(SelectByIdentifierQuery, GetSearchByIdentifierParams(identifier)).SingleOrDefault();
            if (databaseModel == null)
            {
                return false;
            }

            entity = Convert(databaseModel);
            return true;
        }

        public IReadOnlyCollection<TEntity> GetAll()
        {
            using var dbConnection = DbConnectionProvider.Get();
            IEnumerable<TEntityDatabaseModel> databaseModels = dbConnection.Query<TEntityDatabaseModel>(SelectAllQuery);
            return databaseModels.Select(Convert).ToList();
        }

        public void Add(TEntity entity)
        {
            using var dbConnection = DbConnectionProvider.Get();
            dbConnection.Execute(InsertQuery, GetModifyEntityQueryParams(entity));
        }

        public void Update(TEntity entity)
        {
            using var dbConnection = DbConnectionProvider.Get();
            dbConnection.Execute(UpdateQuery, GetModifyEntityQueryParams(entity));
        }

        public void Delete(TIdentifier identifier)
        {
            using var dbConnection = DbConnectionProvider.Get();
            dbConnection.Execute(DeleteQuery, GetSearchByIdentifierParams(identifier));
        }

        protected abstract TEntity Convert(TEntityDatabaseModel databaseModel);

        protected abstract TEntityDatabaseModel Convert(TEntity entity);

        protected virtual object GetSearchByIdentifierParams(TIdentifier identifier) => new { Id = identifier };

        protected virtual object GetModifyEntityQueryParams(TEntity entity) => Convert(entity);
    }
}