using Microsoft.Extensions.DependencyInjection;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Readers;
using TicketFlow.Common.Serializers;

namespace TicketFlow.Common.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPostgresDbConnectionProvider), typeof(PostgresDbConnectionProvider));
            services.AddTransient(typeof(IPostgresConnectionStringProvider), typeof(PostgresConnectionStringProvider));
            services.AddTransient(typeof(IRandomValueProvider), typeof(RandomValueProvider));
            services.AddTransient(typeof(IStringFromStreamReader), typeof(StringFromStreamReader));
            services.AddTransient(typeof(IJsonSerializer), typeof(JsonSerializer));
            services.AddTransient(typeof(IDateTimeProvider), typeof(DateTimeProvider));
            services.AddHttpClient();
        }
    }
}