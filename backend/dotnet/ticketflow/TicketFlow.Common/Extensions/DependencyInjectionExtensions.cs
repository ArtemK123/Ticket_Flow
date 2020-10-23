using Microsoft.Extensions.DependencyInjection;
using TicketFlow.Common.Providers;
using TicketFlow.Common.Readers;
using TicketFlow.Common.Serializers;
using TicketFlow.Common.ServiceUrl.Providers;
using TicketFlow.Common.ServiceUrl.Resolvers;
using TicketFlow.Common.ServiceUrl.Scenarios;

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
            BindServiceUrl(services);
        }

        private static void BindServiceUrl(IServiceCollection services)
        {
            services.AddTransient(typeof(IServiceUrlProvider), typeof(ServiceUrlProvider));

            services.AddTransient(typeof(IServiceUrlProvidingScenarioResolver), typeof(ServiceUrlProvidingScenarioResolver));
            services.AddTransient(typeof(IServiceUrlProvidingTypeProvider), typeof(ServiceUrlProvidingTypeProvider));
            services.AddTransient(typeof(IServiceUrlProvidingScenario), typeof(ServiceUrlFromSettingsScenario));
            services.AddTransient(typeof(IServiceUrlProvidingScenario), typeof(ServiceUrlFromConsulScenario));
        }
    }
}