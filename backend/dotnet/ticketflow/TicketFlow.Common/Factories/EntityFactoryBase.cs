using TicketFlow.Common.Providers;

namespace TicketFlow.Common.Factories
{
    public abstract class EntityFactoryBase<TEntity, TCreationModel> : IEntityFactory<TEntity, TCreationModel>
    {
        private readonly IRandomValueProvider randomValueProvider;

        protected EntityFactoryBase(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public TEntity Create(TCreationModel creationModel)
        {
            int id = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return CreateEntityFromModel(id, creationModel);
        }

        protected abstract TEntity CreateEntityFromModel(int id, TCreationModel creationModel);
    }
}