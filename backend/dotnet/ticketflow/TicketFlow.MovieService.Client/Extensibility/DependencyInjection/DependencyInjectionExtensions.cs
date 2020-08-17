using Microsoft.Extensions.DependencyInjection;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Client.Factories;
using TicketFlow.MovieService.Client.Proxies;
using TicketFlow.MovieService.Client.Serializers;

namespace TicketFlow.MovieService.Client.Extensibility.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddProfileServiceClientServices(this IServiceCollection services)
        {
            BindPublicServices(services);
            BindPrivateServices(services);
        }

        private static void BindPublicServices(IServiceCollection services)
        {
            services.AddTransient(typeof(ICinemaHallFactory), typeof(CinemaHallFactory));
            services.AddTransient(typeof(IFilmFactory), typeof(FilmFactory));
            services.AddTransient(typeof(IMovieFactory), typeof(MovieFactory));
            services.AddTransient(typeof(ICinemaHallApiProxy), typeof(CinemaHallApiProxy));
            services.AddTransient(typeof(IFilmApiProxy), typeof(FilmApiProxy));
            services.AddTransient(typeof(IMovieApiProxy), typeof(MovieApiProxy));
            services.AddTransient(typeof(ICinemaHallSerializer), typeof(CinemaHallSerializer));
            services.AddTransient(typeof(IFilmSerializer), typeof(FilmSerializer));
            services.AddTransient(typeof(IMovieSerializer), typeof(MovieSerializer));
        }

        private static void BindPrivateServices(IServiceCollection services)
        {
        }
    }
}