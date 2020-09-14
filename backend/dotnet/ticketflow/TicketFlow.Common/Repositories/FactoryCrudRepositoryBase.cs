using TicketFlow.Common.Factories;
using TicketFlow.Common.Providers;

namespace TicketFlow.Common.Repositories
{
    public abstract class FactoryCrudRepositoryBase<TIdentifier, TEntity, TEntityDatabaseModel, TEntityFactoryModel>
        : MappedCrudRepositoryBase<TIdentifier, TEntity, TEntityDatabaseModel>
    {
        private readonly IEntityFactory<TEntity, TEntityFactoryModel> entityFactory;

        protected FactoryCrudRepositoryBase(IDbConnectionProvider dbConnectionProvider, IEntityFactory<TEntity, TEntityFactoryModel> entityFactory)
            : base(dbConnectionProvider)
        {
            this.entityFactory = entityFactory;
        }

        protected abstract TEntityFactoryModel ConvertToFactoryModel(TEntityDatabaseModel databaseModel);

        protected override TEntity Convert(TEntityDatabaseModel databaseModel)
        {
            return entityFactory.Create(ConvertToFactoryModel(databaseModel));
        }
    }
}