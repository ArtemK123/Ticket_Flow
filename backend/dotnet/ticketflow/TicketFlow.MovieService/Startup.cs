using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketFlow.Common.Extensions;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService
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
            services.AddConsul(Configuration);
            services.AddControllers();

            services.AddTransient(typeof(ICinemaHallService), typeof(CinemaHallService));
            services.AddTransient(typeof(IFilmService), typeof(FilmService));
            services.AddTransient(typeof(IMovieService), typeof(Service.MovieService));
            services.AddTransient(typeof(ICinemaHallFactory), typeof(CinemaHallFactory));
            services.AddTransient(typeof(IFilmFactory), typeof(FilmFactory));
            services.AddTransient(typeof(IMovieFactory), typeof(MovieFactory));
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