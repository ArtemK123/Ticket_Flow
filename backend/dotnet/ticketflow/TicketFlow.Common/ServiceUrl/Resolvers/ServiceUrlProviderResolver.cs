using System.Collections.Generic;
using System.Linq;
using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Providers;

namespace TicketFlow.Common.ServiceUrl.Resolvers
{
    internal class ServiceUrlProviderResolver : IServiceUrlProviderResolver
    {
        private readonly IEnumerable<IServiceUrlProvider> serviceUrlProviders;

        public ServiceUrlProviderResolver(IEnumerable<IServiceUrlProvider> serviceUrlProviders)
        {
            this.serviceUrlProviders = serviceUrlProviders;
        }

        public IServiceUrlProvider Resolve(ServiceUrlProvidingType providingType)
            => serviceUrlProviders.Single(serviceUrlProvider => serviceUrlProvider.ProvidingType == providingType);
    }
}