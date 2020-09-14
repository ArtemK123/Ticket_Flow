using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketFlow.Common.Providers;

namespace TicketFlow.TicketService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    IConfigurationRoot appsettings =
                        new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

                    IUrlFromConfigProvider urlFromConfigProvider = new UrlFromConfigProvider();

                    webBuilder.UseUrls(urlFromConfigProvider.GetUrl(appsettings));
                    webBuilder.UseStartup<Startup>();
                });
    }
}