using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketFlow.Common.Extensions;
using TicketFlow.ProfileService.Client.Extensibility.DependencyInjection;
using TicketFlow.ProfileService.Persistence.Repositories;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCommonServices();
            services.AddProfileService();
            services.AddFluentMigrator(Configuration, typeof(Startup).Assembly);
            services.AddTransient(typeof(IProfileRepository), typeof(ProfileRepository));
            services.AddTransient(typeof(IProfileService), typeof(Service.ProfileService));

            services.AddControllers();
            services.AddConsul(Configuration);
        }

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
    }
}