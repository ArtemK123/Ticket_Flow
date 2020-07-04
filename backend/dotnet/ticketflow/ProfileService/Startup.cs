using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfileService.Domain;
using ProfileService.Domain.Repositories;
using ProfileService.Service;

namespace ProfileService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddDomain(services);
            AddService(services);
            AddApi(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void AddApi(IServiceCollection services)
        {
            services.AddControllers();
        }

        private static void AddService(IServiceCollection services)
        {
            services.AddTransient(typeof(IProfileService), typeof(Service.ProfileService));
        }

        private static void AddDomain(IServiceCollection services)
        {
            services.AddTransient(typeof(IDbConnectionProvider), typeof(NpgsqlConnectionProvider));
            services.AddTransient(typeof(IProfileRepository), typeof(ProfileRepository));
        }
    }
}