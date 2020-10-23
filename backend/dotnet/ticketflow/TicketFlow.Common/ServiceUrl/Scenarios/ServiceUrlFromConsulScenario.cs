using System;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Scenarios
{
    internal class ServiceUrlFromConsulScenario : IServiceUrlProvidingScenario
    {
        private readonly IConfiguration configuration;
        private readonly IConsulClient consulClient;

        public ServiceUrlFromConsulScenario(IConfiguration configuration, IConsulClient consulClient)
        {
            this.configuration = configuration;
            this.consulClient = consulClient;
        }

        public ServiceUrlProvidingType ProvidingType => ServiceUrlProvidingType.FromConsul;

        public async Task<string> GetUrlAsync(string serviceName)
        {
            var consulServiceNameSettingPath = $"TicketFlow:{serviceName}:ConsulName";

            string consulServiceName = configuration.GetValue<string>(consulServiceNameSettingPath);

            var allServices = await consulClient.Agent.Services();
            throw new NotImplementedException();
        }
    }
}