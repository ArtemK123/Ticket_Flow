using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Scenarios;

namespace TicketFlow.Common.ServiceUrl.Resolvers
{
    public interface IServiceUrlProvidingScenarioResolver
    {
        IServiceUrlProvidingScenario Resolve(ServiceUrlProvidingType providingType);
    }
}