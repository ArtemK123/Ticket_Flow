namespace TicketFlow.Common.Serializers
{
    public interface IEntitySerializer<TEntity, TSerializationModel>
    {
        TSerializationModel Serialize(TEntity entity);

        TEntity Deserialize(TSerializationModel serializationModel);
    }
}