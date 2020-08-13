using Microsoft.Extensions.DependencyInjection;
using TicketFlow.ProfileService.Client.Extensibility.Factories;
using TicketFlow.ProfileService.Client.Extensibility.Proxies;
using TicketFlow.ProfileService.Client.Extensibility.Serializers;
using TicketFlow.ProfileService.Client.Factories;
using TicketFlow.ProfileService.Client.Proxies;
using TicketFlow.ProfileService.Client.Serializers;

namespace TicketFlow.ProfileService.Client.Extensibility.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddProfileServiceClientServices(this IServiceCollection services)
        {
            BindPublicServices(services);
        }

        private static void BindPublicServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IProfileFactory), typeof(ProfileFactory));
            services.AddTransient(typeof(IProfileSerializer), typeof(ProfileSerializer));
            services.AddTransient(typeof(IProfileApiProxy), typeof(ProfileApiProxy));
        }
    }
}