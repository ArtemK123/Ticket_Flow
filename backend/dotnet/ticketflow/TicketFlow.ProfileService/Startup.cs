using System;
using Consul;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketFlow.Common.Extensions;
using TicketFlow.ProfileService.Domain.Providers;
using TicketFlow.ProfileService.Domain.Repositories;
using TicketFlow.ProfileService.Service;

namespace TicketFlow.ProfileService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime, IServiceProvider serviceProvider)
        {
            app.UseExceptionHandler("/error");

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.RegisterWithConsul(lifetime, Configuration);

            var migrationRunner = serviceProvider.GetRequiredService<IMigrationRunner>();
            migrationRunner.MigrateUp();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddDomain(services);
            AddService(services);
            AddApi(services);
            AddConsul(services);
        }

        private static void AddService(IServiceCollection services)
        {
            services.AddTransient(typeof(IProfileService), typeof(Service.ProfileService));
        }

        private static void AddApi(IServiceCollection services)
        {
            services.AddControllers();
        }

        private void AddConsul(IServiceCollection services)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = Configuration["Consul:Address"];
                consulConfig.Address = new Uri(address);
            }));
        }

        private void AddDomain(IServiceCollection services)
        {
            services.AddTransient(typeof(IPostgresConnectionStringProvider), typeof(PostgresConnectionStringProvider));
            services.AddTransient(typeof(IPosgtresDbConnectionProvider), typeof(PostgresDbConnectionProvider));
            services.AddTransient(typeof(IProfileRepository), typeof(ProfileRepository));
            services.AddTransient(typeof(IRandomValueProvider), typeof(RandomValueProvider));

            IPostgresConnectionStringProvider connectionStringProvider = new PostgresConnectionStringProvider(Configuration);

            services.AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddPostgres()
                        .WithGlobalConnectionString(connectionStringProvider.GetConnectionString())
                        .ScanIn(typeof(Startup).Assembly).For.Migrations());
        }
    }
}