using Microsoft.Extensions.DependencyInjection;
using TicketFlow.Common.Providers;

namespace TicketFlow.Common.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPostgresDbConnectionProvider), typeof(PostgresDbConnectionProvider));
            services.AddTransient(typeof(IPostgresConnectionStringProvider), typeof(PostgresConnectionStringProvider));
            services.AddTransient(typeof(IRandomValueProvider), typeof(RandomValueProvider));
        }
    }
}