using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TicketFlow.ApiGateway.Proxy;
using TicketFlow.ApiGateway.Service;

namespace TicketFlow.ApiGateway
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IProfilesApiProxy), typeof(ProfilesApiProxy));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}