using System;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Scenarios
{
    internal class ServiceUrlFromSettingsScenario : IServiceUrlProvidingScenario
    {
        private readonly IConfiguration configuration;

        public ServiceUrlFromSettingsScenario(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ServiceUrlProvidingType ProvidingType => ServiceUrlProvidingType.FromSettings;

        public string GetUrl(string serviceName)
        {
            var urlSettingPath = $"TicketFlow:{serviceName}:Url";

            string url = configuration.GetValue<string>(urlSettingPath);

            if (string.IsNullOrEmpty(url))
            {
                throw new Exception($"Please, specify the url for service {serviceName} in the configuration file by the path {urlSettingPath}");
            }

            return url;
        }
    }
}