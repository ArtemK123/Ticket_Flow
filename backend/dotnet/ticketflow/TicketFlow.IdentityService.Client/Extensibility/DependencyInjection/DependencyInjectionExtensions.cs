using Microsoft.Extensions.DependencyInjection;
using TicketFlow.IdentityService.Client.Extensibility.Factories;
using TicketFlow.IdentityService.Client.Extensibility.Proxies;
using TicketFlow.IdentityService.Client.Extensibility.Serializers;
using TicketFlow.IdentityService.Client.Factories;
using TicketFlow.IdentityService.Client.Generators;
using TicketFlow.IdentityService.Client.Providers;
using TicketFlow.IdentityService.Client.Proxies;
using TicketFlow.IdentityService.Client.Senders;
using TicketFlow.IdentityService.Client.Serializers;
using TicketFlow.IdentityService.Client.Validators;

namespace TicketFlow.IdentityService.Client.Extensibility.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddIdentityService(this IServiceCollection services)
        {
            BindPublicServices(services);
            BindPrivateServices(services);
        }

        private static void BindPublicServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IUserFactory), typeof(UserFactory));
            services.AddTransient(typeof(IUserSerializer), typeof(UserSerializer));
            services.AddTransient(typeof(IUserApiProxy), typeof(UserApiProxy));
        }

        private static void BindPrivateServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IJwtGenerator), typeof(JwtGenerator));
            services.AddTransient(typeof(IIdentityServiceResponseValidator), typeof(IdentityServiceResponseValidator));
            services.AddTransient(typeof(IIdentityServiceMessageSender), typeof(IdentityServiceMessageSender));
            services.AddTransient(typeof(IIdentityServiceUrlProvider), typeof(IdentityServiceUrlProvider));
        }
    }
}