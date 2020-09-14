using System;
using Microsoft.Extensions.Configuration;

namespace TicketFlow.Common.Providers
{
    // public because can be used in Program.cs before the startup, so should be created directly
    public class UrlFromConfigProvider : IUrlFromConfigProvider
    {
        private const int MinimumPortValue = 1024;
        private const int MaximumPortValue = 64000;

        private readonly IRandomValueProvider randomValueProvider;

        public UrlFromConfigProvider()
        {
            // direct initialization in order to decrease the number of public classes, which are referenced in Program.cs
            randomValueProvider = new RandomValueProvider();
        }

        public string GetUrl(IConfigurationRoot configuration)
        {
            // if bool value is not found in configs - false is returned
            bool runOnRandomPort = configuration.GetValue<bool>("TicketFlow:RunOnRandomPort");

            int port = runOnRandomPort
                ? randomValueProvider.GetRandomInt(MinimumPortValue, MaximumPortValue)
                : GetPortFromConfig(configuration);

            string urlBase = GetUrlBaseFromConfig(configuration);
            return $"{urlBase}:{port}";
        }

        private int GetPortFromConfig(IConfigurationRoot configuration)
        {
            int port = configuration.GetValue<int>("TicketFlow:Port");
            if (port == default)
            {
                throw new Exception("Application port is wrongly configured. Please, setup configuration file with TicketFlow:Port or set TicketFlow:RunOnRandomPort=true");
            }

            if (port < MinimumPortValue)
            {
                throw new Exception($"Application port is wrongly configured. Port value is lower than allowed {MinimumPortValue}");
            }

            if (port > MaximumPortValue)
            {
                throw new Exception($"Application port is wrongly configured. Port value is higher than allowed {MaximumPortValue}");
            }

            return port;
        }

        private string GetUrlBaseFromConfig(IConfigurationRoot configuration)
        {
            string urlBase = configuration.GetValue<string>("TicketFlow:ServiceBaseUrl");
            if (string.IsNullOrEmpty(urlBase))
            {
                throw new Exception("Application port is wrongly configured. Please, specify TicketFlow:ServiceBaseUrl in configuration file");
            }

            return urlBase;
        }
    }
}