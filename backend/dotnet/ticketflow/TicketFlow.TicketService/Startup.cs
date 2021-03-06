using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketFlow.Common.Extensions;
using TicketFlow.TicketService.Client.Extensibility.DependencyInjection;
using TicketFlow.TicketService.Persistence;
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

            services.AddTicketService();
            services.AddTransient(typeof(ITicketService), typeof(Service.TicketService));
            services.AddTransient(typeof(ITicketRepository), typeof(TicketRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            app.UseExceptionHandler("/error");

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseConsul(lifetime, Configuration);

            app.UseFluentMigrator(lifetime);
        }
    }
}