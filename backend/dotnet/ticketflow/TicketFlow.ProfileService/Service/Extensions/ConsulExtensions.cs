using System;
using System.Linq;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProfileService.Service.Extensions
{
    public static class ConsulExtensions
    {
        public static void RegisterWithConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            AgentServiceRegistration registration = GetServiceRegistration(app, configuration);
            var logger = app.ApplicationServices.GetRequiredService<ILogger<IConsulClient>>();

            IConsulClient consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });
        }

        private static AgentServiceRegistration GetServiceRegistration(IApplicationBuilder app, IConfiguration configuration)
        {
            FeatureCollection features = app.Properties["server.Features"] as FeatureCollection;
            IServerAddressesFeature addresses = features.Get<IServerAddressesFeature>();
            string address = addresses.Addresses.First();

            string serviceName = configuration["Consul:ServiceName"];
            string[] serviceTags = configuration.GetSection("Consul:ServiceTags").GetChildren().Select(c => c.Value).ToArray();
            var serviceId = $"{serviceName}:{Guid.NewGuid()}";

            var uri = new Uri(address);
            var registration = new AgentServiceRegistration
            {
                ID = $"{serviceId}-{uri.Port}",
                Name = serviceName,
                Address = $"{uri.Scheme}://{uri.Host}",
                Port = uri.Port,
                Tags = serviceTags
            };
            return registration;
        }
    }
}