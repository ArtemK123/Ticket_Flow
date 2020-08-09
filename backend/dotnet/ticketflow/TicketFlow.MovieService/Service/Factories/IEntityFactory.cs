namespace TicketFlow.MovieService.Service.Factories
{
    public interface IEntityFactory<out TEntity, in TCreationModel>
    {
        TEntity Create(TCreationModel creationModel);
    }
}