namespace TicketFlow.ProfileService.Domain.Providers
{
    internal interface IPostgresConnectionStringProvider
    {
        string GetConnectionString();
    }
}