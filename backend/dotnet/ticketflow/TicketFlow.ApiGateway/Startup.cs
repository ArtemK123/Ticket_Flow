using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketFlow.ApiGateway.Service;
using TicketFlow.ApiGateway.StartupServices;
using TicketFlow.ApiGateway.StartupServices.Seeders;
using TicketFlow.ApiGateway.WebApi.TicketsApi.Converters;
using TicketFlow.Common.Extensions;
using TicketFlow.IdentityService.Client.Extensibility.DependencyInjection;
using TicketFlow.MovieService.Client.Extensibility.DependencyInjection;
using TicketFlow.ProfileService.Client.Extensibility.DependencyInjection;
using TicketFlow.TicketService.Client.Extensibility.DependencyInjection;

namespace TicketFlow.ApiGateway
{
    public class Startup
    {
        private const string FrontendAppCorsPolicyName = "FrontendApp";

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
            services.AddCors(options =>
            {
                options.AddPolicy(
                    FrontendAppCorsPolicyName,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000");
                    });
            });
            services.AddConsul(Configuration);

            services.AddTransient(typeof(ITicketWithMovieService), typeof(TicketWithMovieService));
            services.AddTransient(typeof(ITicketWithMovieConverter), typeof(TicketWithMovieConverter));
            services.AddTransient(typeof(IOrderTicketUseCase), typeof(OrderTicketUseCase));
            services.AddTransient(typeof(IUserWithProfileService), typeof(UserWithProfileService));
            services.AddTransient(typeof(IAddMovieUseCase), typeof(AddMovieUseCase));

            services.AddTransient(typeof(ICinemaHallsSeeder), typeof(CinemaHallsSeeder));
            services.AddTransient(typeof(IFilmsSeeder), typeof(FilmsSeeder));
            services.AddTransient(typeof(IMoviesSeeder), typeof(MoviesSeeder));
            services.AddTransient(typeof(ISeederRunner), typeof(SeederRunner));
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime, IServiceProvider serviceProvider)
        {
            app.UseExceptionHandler("/error");

            app.UseRouting();

            app.UseCors(FrontendAppCorsPolicyName);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.RegisterWithConsul(lifetime, Configuration);

            ISeederRunner seederRunner = serviceProvider.GetService<ISeederRunner>();
            seederRunner.RunSeedersAsync().Wait();
        }
    }
}