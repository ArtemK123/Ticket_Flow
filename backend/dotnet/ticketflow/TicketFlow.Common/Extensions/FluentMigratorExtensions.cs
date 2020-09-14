using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketFlow.Common.Providers;

namespace TicketFlow.Common.Extensions
{
    public static class FluentMigratorExtensions
    {
        public static void AddFluentMigrator(this IServiceCollection services, IConfiguration configuration, Assembly assemblyWithMigrations)
        {
            IPostgresConnectionStringProvider connectionStringProvider = new PostgresConnectionStringProvider(configuration);

            services.AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddPostgres()
                        .WithGlobalConnectionString(connectionStringProvider.GetConnectionString())
                        .ScanIn(assemblyWithMigrations).For.Migrations());
        }
    }
}