using System.Collections.Generic;
using System.Linq;
using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Scenarios;

namespace TicketFlow.Common.ServiceUrl.Resolvers
{
    internal class ServiceUrlProvidingScenarioResolver : IServiceUrlProvidingScenarioResolver
    {
        private readonly IEnumerable<IServiceUrlProvidingScenario> serviceUrlProviders;

        public ServiceUrlProvidingScenarioResolver(IEnumerable<IServiceUrlProvidingScenario> serviceUrlProviders)
        {
            this.serviceUrlProviders = serviceUrlProviders;
        }

        public IServiceUrlProvidingScenario Resolve(ServiceUrlProvidingType providingType)
            => serviceUrlProviders.Single(serviceUrlProvider => serviceUrlProvider.ProvidingType == providingType);
    }
}