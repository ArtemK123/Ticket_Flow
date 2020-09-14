using TicketFlow.Common.Providers;
using TicketFlow.Common.Serializers;

namespace TicketFlow.Common.Repositories
{
    public abstract class SerializingCrudRepositoryBase<TIdentifier, TEntity, TEntitySerializationModel>
        : MappedCrudRepositoryBase<TIdentifier, TEntity, TEntitySerializationModel>
    {
        private readonly IEntitySerializer<TEntity, TEntitySerializationModel> entitySerializer;

        protected SerializingCrudRepositoryBase(IDbConnectionProvider dbConnectionProvider, IEntitySerializer<TEntity, TEntitySerializationModel> entitySerializer)
            : base(dbConnectionProvider)
        {
            this.entitySerializer = entitySerializer;
        }

        protected override TEntity Convert(TEntitySerializationModel databaseModel) => entitySerializer.Deserialize(databaseModel);

        protected override TEntitySerializationModel Convert(TEntity entity) => entitySerializer.Serialize(entity);
    }
}