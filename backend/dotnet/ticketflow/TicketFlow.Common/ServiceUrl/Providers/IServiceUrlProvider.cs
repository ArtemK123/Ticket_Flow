namespace TicketFlow.Common.ServiceUrl.Providers
{
    public interface IServiceUrlProvider
    {
        string GetUrl(string serviceName);
    }
}