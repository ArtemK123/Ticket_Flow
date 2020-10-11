using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Scenarios
{
    public interface IServiceUrlProvidingScenario
    {
        ServiceUrlProvidingType ProvidingType { get; }

        string GetUrl(string serviceName);
    }
}