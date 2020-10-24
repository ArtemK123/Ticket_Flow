using System.Threading.Tasks;
using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Resolvers;
using TicketFlow.Common.ServiceUrl.Scenarios;

namespace TicketFlow.Common.ServiceUrl.Providers
{
    internal class ServiceUrlProvider : IServiceUrlProvider
    {
        private readonly IServiceUrlProvidingTypeProvider serviceUrlProvidingTypeProvider;
        private readonly IServiceUrlProvidingScenarioResolver serviceUrlProvidingScenarioResolver;

        public ServiceUrlProvider(IServiceUrlProvidingTypeProvider serviceUrlProvidingTypeProvider, IServiceUrlProvidingScenarioResolver serviceUrlProvidingScenarioResolver)
        {
            this.serviceUrlProvidingTypeProvider = serviceUrlProvidingTypeProvider;
            this.serviceUrlProvidingScenarioResolver = serviceUrlProvidingScenarioResolver;
        }

        public Task<string> GetUrlAsync(string serviceName)
        {
            ServiceUrlProvidingType providingType = serviceUrlProvidingTypeProvider.GetServiceUrlResolvingType();
            IServiceUrlProvidingScenario providingScenario = serviceUrlProvidingScenarioResolver.Resolve(providingType);
            return providingScenario.GetUrlAsync(serviceName);
        }
    }
}