using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketFlow.Common.Extensions;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;
using TicketFlow.MovieService.Domain.Models.FilmModels;
using TicketFlow.MovieService.Domain.Models.MovieModels;
using TicketFlow.MovieService.Persistence;
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
            services.AddTransient(typeof(IEntityFactory<ICinemaHall, CinemaHallCreationModel>), typeof(CinemaHallFactory));
            services.AddTransient(typeof(IEntityFactory<ICinemaHall, StoredCinemaHallCreationModel>), typeof(CinemaHallFactory));
            services.AddTransient(typeof(IEntityFactory<IFilm, FilmCreationModel>), typeof(FilmFactory));
            services.AddTransient(typeof(IEntityFactory<IFilm, StoredFilmCreationModel>), typeof(FilmFactory));
            services.AddTransient(typeof(IEntityFactory<IMovie, MovieCreationModel>), typeof(MovieFactory));
            services.AddTransient(typeof(IEntityFactory<IMovie, StoredMovieCreationModel>), typeof(MovieFactory));
            services.AddTransient(typeof(ICinemaHallRepository), typeof(CinemaHallRepository));
            services.AddTransient(typeof(IFilmRepository), typeof(FilmRepository));
            services.AddTransient(typeof(IMovieRepository), typeof(MovieRepository));
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