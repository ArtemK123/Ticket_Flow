using Microsoft.Extensions.DependencyInjection;
using TicketFlow.ProfileService.Client.Extensibility.Factories;
using TicketFlow.ProfileService.Client.Extensibility.Proxies;
using TicketFlow.ProfileService.Client.Extensibility.Serializers;
using TicketFlow.ProfileService.Client.Factories;
using TicketFlow.ProfileService.Client.Proxies;
using TicketFlow.ProfileService.Client.Senders;
using TicketFlow.ProfileService.Client.Serializers;
using TicketFlow.ProfileService.Client.Validators;

namespace TicketFlow.ProfileService.Client.Extensibility.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddProfileService(this IServiceCollection services)
        {
            BindPublicServices(services);
            BindPrivateServices(services);
        }

        private static void BindPublicServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IProfileFactory), typeof(ProfileFactory));
            services.AddTransient(typeof(IProfileSerializer), typeof(ProfileSerializer));
            services.AddTransient(typeof(IProfileApiProxy), typeof(ProfileApiProxy));
        }

        private static void BindPrivateServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IProfileServiceResponseValidator), typeof(ProfileServiceResponseValidator));
            services.AddTransient(typeof(IProfileServiceMessageSender), typeof(ProfileServiceMessageSender));
        }
    }
}