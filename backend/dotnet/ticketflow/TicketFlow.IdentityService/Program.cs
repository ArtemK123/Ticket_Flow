using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketFlow.Common.Providers;

namespace TicketFlow.IdentityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(GetUrlFromConfigs());
                    webBuilder.UseStartup<Startup>();
                });

        private static string GetUrlFromConfigs()
        {
            IConfigurationRoot appSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            bool runOnRandomPort = appSettings.GetValue<bool>("TicketFlow:RunOnRandomPort");
            int port;

            if (runOnRandomPort)
            {
                IRandomValueProvider randomValueProvider = new RandomValueProvider();
                port = randomValueProvider.GetRandomInt(1024, 50000);
            }
            else
            {
                port = appSettings.GetValue<int>("TicketFlow:Port");
            }

            string urlBase = appSettings.GetValue<string>("TicketFlow:ServiceBaseUrl");
            return $"{urlBase}:{port}";
        }
    }
}