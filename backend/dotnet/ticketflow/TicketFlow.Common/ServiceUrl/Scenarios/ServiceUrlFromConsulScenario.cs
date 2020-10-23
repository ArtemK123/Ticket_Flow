using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.Providers;
using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Scenarios
{
    internal class ServiceUrlFromConsulScenario : IServiceUrlProvidingScenario
    {
        private readonly IConfiguration configuration;
        private readonly IConsulClient consulClient;
        private readonly IRandomValueProvider randomValueProvider;

        public ServiceUrlFromConsulScenario(IConfiguration configuration, IConsulClient consulClient, IRandomValueProvider randomValueProvider)
        {
            this.configuration = configuration;
            this.consulClient = consulClient;
            this.randomValueProvider = randomValueProvider;
        }

        public ServiceUrlProvidingType ProvidingType => ServiceUrlProvidingType.FromConsul;

        public async Task<string> GetUrlAsync(string serviceName)
        {
            var consulServiceNameSettingPath = $"TicketFlow:{serviceName}:ConsulName";

            string consulServiceName = configuration.GetValue<string>(consulServiceNameSettingPath);

            QueryResult<Dictionary<string, AgentService>> allServices = await consulClient.Agent.Services();

            IReadOnlyCollection<AgentService> suitableServices =
                allServices.Response?.Where(s => s.Value.Service.Equals(consulServiceName, StringComparison.Ordinal)).Select(x => x.Value).ToList();

            if (suitableServices == null || suitableServices.Count == 0)
            {
                throw new ServiceNotFoundException(serviceName);
            }

            int takenServiceId = randomValueProvider.GetRandomInt(0, suitableServices.Count);
            AgentService service = suitableServices.ElementAt(takenServiceId);
            return service.Address;
        }
    }
}