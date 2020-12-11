using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TicketFlow.ApiGateway.StartupServices
{
    internal static class ApiGatewayStartupExtensions
    {
        public static IApplicationBuilder UseApiGatewaySeeders(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            try
            {
                IApiGatewayStartupSeeder apiGatewayStartupSeeder = app.ApplicationServices.GetRequiredService<IApiGatewayStartupSeeder>();
                apiGatewayStartupSeeder.SeedAsync().Wait();
            }
            catch (Exception)
            {
                ILogger<IApiGatewayStartupSeeder> logger = app.ApplicationServices.GetRequiredService<ILogger<IApiGatewayStartupSeeder>>();
                logger.LogCritical("Error during startup. Application will stop!");
                lifetime.StopApplication();
                throw;
            }

            return app;
        }
    }
}