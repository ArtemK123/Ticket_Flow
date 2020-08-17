using Microsoft.Extensions.DependencyInjection;
using TicketFlow.IdentityService.Client.Extensibility.Factories;
using TicketFlow.IdentityService.Client.Extensibility.Serializers;
using TicketFlow.IdentityService.Client.Factories;
using TicketFlow.IdentityService.Client.Generators;
using TicketFlow.IdentityService.Client.Serializers;

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
        }

        private static void BindPrivateServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IJwtGenerator), typeof(JwtGenerator));
        }
    }
}