using System.Collections.Generic;
using System.Linq;
using Dapper;
using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService.Persistence
{
    public abstract class PostgresCrudRepositoryBase<TIdentifier, TEntity, TEntityFactoryModel, TEntityDatabaseModel> : ICrudRepository<TIdentifier, TEntity>
    {
        protected PostgresCrudRepositoryBase(IPostgresDbConnectionProvider dbConnectionProvider, IEntityFactory<TEntity, TEntityFactoryModel> entityFactory)
        {
            DbConnectionProvider = dbConnectionProvider;
            EntityFactory = entityFactory;
        }

        protected abstract string SelectByIdentifierQuery { get; }

        protected abstract string SelectAllQuery { get; }

        protected abstract string InsertQuery { get; }

        protected abstract string UpdateQuery { get; }

        protected abstract string DeleteQuery { get; }

        protected IPostgresDbConnectionProvider DbConnectionProvider { get; }

        protected IEntityFactory<TEntity, TEntityFactoryModel> EntityFactory { get; }

        public bool TryGet(TIdentifier identifier, out TEntity entity)
        {
            entity = default;
            using var dbConnection = DbConnectionProvider.Get();
            TEntityDatabaseModel databaseModel = dbConnection.Query<TEntityDatabaseModel>(SelectByIdentifierQuery, new { identifier }).SingleOrDefault();
            if (databaseModel == null)
            {
                return false;
            }

            entity = CreateWithFactory(databaseModel);
            return true;
        }

        public IReadOnlyCollection<TEntity> GetAll()
        {
            using var dbConnection = DbConnectionProvider.Get();
            IEnumerable<TEntityDatabaseModel> databaseModels = dbConnection.Query<TEntityDatabaseModel>(SelectAllQuery);
            return databaseModels.Select(CreateWithFactory).ToList();
        }

        public void Add(TEntity entity)
        {
            using var dbConnection = DbConnectionProvider.Get();
            dbConnection.Execute(InsertQuery, Convert(entity));
        }

        public void Update(TEntity entity)
        {
            using var dbConnection = DbConnectionProvider.Get();
            dbConnection.Execute(UpdateQuery, Convert(entity));
        }

        public void Delete(TIdentifier identifier)
        {
            using var dbConnection = DbConnectionProvider.Get();
            dbConnection.Execute(DeleteQuery, new { identifier });
        }

        protected abstract TEntityFactoryModel Convert(TEntityDatabaseModel databaseModel);

        protected abstract TEntityDatabaseModel Convert(TEntity entity);

        private TEntity CreateWithFactory(TEntityDatabaseModel entityDatabaseModel) => EntityFactory.Create(Convert(entityDatabaseModel));
    }
}