using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketFlow.Common.Extensions;
using TicketFlow.IdentityService.Client.Extensibility.DependencyInjection;
using TicketFlow.MovieService.Client.Extensibility.DependencyInjection;
using TicketFlow.ProfileService.Client.Extensibility.DependencyInjection;
using TicketFlow.TicketService.Client.Extensibility.DependencyInjection;

namespace TicketFlow.ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCommonServices();
            services.AddIdentityService();
            services.AddProfileService();
            services.AddTicketService();
            services.AddMovieService();

            services.AddControllers();
            services.AddHttpClient();
            services.AddConsul(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            app.UseExceptionHandler("/error");

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.RegisterWithConsul(lifetime, Configuration);
        }
    }
}