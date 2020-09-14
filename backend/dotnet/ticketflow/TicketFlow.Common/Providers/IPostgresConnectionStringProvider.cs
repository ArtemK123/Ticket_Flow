namespace TicketFlow.Common.Providers
{
    public interface IPostgresConnectionStringProvider
    {
        string GetConnectionString();
    }
}