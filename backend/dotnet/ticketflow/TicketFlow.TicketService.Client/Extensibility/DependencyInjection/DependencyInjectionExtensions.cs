using Microsoft.Extensions.DependencyInjection;
using TicketFlow.TicketService.Client.Extensibility.Factories;
using TicketFlow.TicketService.Client.Extensibility.Proxies;
using TicketFlow.TicketService.Client.Extensibility.Serializers;
using TicketFlow.TicketService.Client.Factories;
using TicketFlow.TicketService.Client.Proxies;
using TicketFlow.TicketService.Client.Senders;
using TicketFlow.TicketService.Client.Validators;

namespace TicketFlow.TicketService.Client.Extensibility.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddTicketServiceClientServices(this IServiceCollection services)
        {
            BindPublicServices(services);
            BindInternalServices(services);
        }

        private static void BindPublicServices(IServiceCollection services)
        {
            services.AddTransient(typeof(ITicketFactory), typeof(TicketFactory));
            services.AddTransient(typeof(ITicketSerializer), typeof(TicketSerializer));
            services.AddTransient(typeof(ITicketApiProxy), typeof(TicketApiProxy));
        }

        private static void BindInternalServices(IServiceCollection services)
        {
            services.AddTransient(typeof(ITicketCreationModelValidator), typeof(TicketCreationModelValidator));
            services.AddTransient(typeof(ITicketServiceResponseValidator), typeof(TicketServiceResponseValidator));
            services.AddTransient(typeof(ITicketServiceMessageSender), typeof(TicketServiceMessageSender));
        }
    }
}