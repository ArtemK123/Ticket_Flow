using System;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

        public static void UseFluentMigrator(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            try
            {
                IMigrationRunner migrationRunner = app.ApplicationServices.GetRequiredService<IMigrationRunner>();
                migrationRunner.MigrateUp();
            }
            catch (Exception)
            {
                ILogger<IMigrationRunner> logger = app.ApplicationServices.GetRequiredService<ILogger<IMigrationRunner>>();
                logger.LogCritical("Error during startup. Application will stop!");
                lifetime.StopApplication();
                throw;
            }
        }
    }
}