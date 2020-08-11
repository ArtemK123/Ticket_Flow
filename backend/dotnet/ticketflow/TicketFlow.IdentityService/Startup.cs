using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketFlow.Common.Extensions;
using TicketFlow.Common.Factories;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Domain.Models;
using TicketFlow.IdentityService.Persistence;
using TicketFlow.IdentityService.Service;
using TicketFlow.IdentityService.Service.Factories;
using TicketFlow.IdentityService.Service.Serializers;

namespace TicketFlow.IdentityService
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
            services.AddControllers();

            services.AddCommonServices();

            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IJwtGenerator), typeof(JwtGenerator));
            services.AddTransient(typeof(IDateTimeProvider), typeof(DateTimeProvider));
            services.AddTransient(typeof(IEntityFactory<IUser, UserCreationModel>), typeof(UserFactory));
            services.AddTransient(typeof(IUserFactory), typeof(UserFactory));
            services.AddTransient(typeof(IUserSerializer), typeof(UserSerializer));

            services.AddConsul(Configuration);

            services.AddFluentMigrator(Configuration, typeof(Startup).Assembly);
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