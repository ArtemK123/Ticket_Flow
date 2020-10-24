using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Scenarios;

namespace TicketFlow.Common.ServiceUrl.Resolvers
{
    internal interface IServiceUrlProvidingScenarioResolver
    {
        IServiceUrlProvidingScenario Resolve(ServiceUrlProvidingType providingType);
    }
}