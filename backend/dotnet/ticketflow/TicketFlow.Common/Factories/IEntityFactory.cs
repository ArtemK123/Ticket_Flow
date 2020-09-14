namespace TicketFlow.Common.Factories
{
    public interface IEntityFactory<out TEntity, in TCreationModel>
    {
        TEntity Create(TCreationModel creationModel);
    }
}