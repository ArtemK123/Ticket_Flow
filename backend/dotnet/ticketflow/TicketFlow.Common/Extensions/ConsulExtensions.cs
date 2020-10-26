using System;
using System.Linq;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TicketFlow.Common.Extensions
{
    public static class ConsulExtensions
    {
        public static void RegisterWithConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            AgentServiceRegistration registration = GetServiceRegistration(configuration);
            ILogger<IConsulClient> logger = app.ApplicationServices.GetRequiredService<ILogger<IConsulClient>>();
            IConsulClient consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                logger.LogInformation("Deregistered from Consul");
            });
        }

        public static void AddConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                string address = configuration.GetValue<string>("Consul:Address");
                consulConfig.Address = new Uri(address);
            }));
        }

        private static AgentServiceRegistration GetServiceRegistration(IConfiguration configuration)
        {
            string serviceName = configuration.GetValue<string>("Consul:ServiceName");
            string serviceUrl = configuration.GetValue<string>("Consul:ServiceUrl");
            string[] serviceTags = configuration.GetSection("Consul:ServiceTags").GetChildren().Select(c => c.Value).ToArray();
            var serviceId = $"{serviceName}:{Guid.NewGuid()}";

            var uri = new Uri(serviceUrl);
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