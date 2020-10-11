using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Providers
{
    public interface IServiceUrlProvider
    {
        ServiceUrlProvidingType ProvidingType { get; }

        string GetUrl(string serviceName);
    }
}