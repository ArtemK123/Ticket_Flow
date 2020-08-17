using Microsoft.Extensions.DependencyInjection;

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
        }

        private static void BindPrivateServices(IServiceCollection services)
        {
        }
    }
}