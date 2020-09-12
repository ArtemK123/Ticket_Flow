using Microsoft.Extensions.Configuration;

namespace TicketFlow.Common.Providers
{
    // public because can be used in Program.cs before the startup, so should be created directly
    public class UrlFromConfigProvider : IUrlFromConfigProvider
    {
        private const int MinimumPortValue = 1024;
        private const int MaximumPortValue = 50000;

        public string GetUrl(IConfigurationRoot configuration)
        {
            bool runOnRandomPort = configuration.GetValue<bool>("TicketFlow:RunOnRandomPort");
            int port;

            if (runOnRandomPort)
            {
                // direct initialization in order to decrease the number of public classes, which are referenced in Program.cs
                IRandomValueProvider randomValueProvider = new RandomValueProvider();
                port = randomValueProvider.GetRandomInt(MinimumPortValue, MaximumPortValue);
            }
            else
            {
                port = configuration.GetValue<int>("TicketFlow:Port");
            }

            string urlBase = configuration.GetValue<string>("TicketFlow:ServiceBaseUrl");
            return $"{urlBase}:{port}";
        }
    }
}