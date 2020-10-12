using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Scenarios
{
    internal interface IServiceUrlProvidingScenario
    {
        ServiceUrlProvidingType ProvidingType { get; }

        string GetUrl(string serviceName);
    }
}