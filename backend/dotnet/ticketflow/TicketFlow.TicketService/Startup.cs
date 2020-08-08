using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketFlow.Common.Extensions;
using TicketFlow.TicketService.Service;

namespace TicketFlow.TicketService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCommonServices();
            services.AddFluentMigrator(Configuration, typeof(Startup).Assembly);
            services.AddControllers();
            services.AddConsul(Configuration);

            services.AddTransient(typeof(ITicketFactory), typeof(TicketFactory));
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