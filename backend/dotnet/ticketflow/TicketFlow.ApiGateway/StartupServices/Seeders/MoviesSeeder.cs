using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using TicketFlow.ApiGateway.Service;
using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;

namespace TicketFlow.ApiGateway.StartupServices.Seeders
{
    [SuppressMessage("SpacingRules", "SA1009", Justification = "Valid bracket placing for collection of tuples")]
    internal class MoviesSeeder : IMoviesSeeder
    {
        private const int MoviesCount = 30;

        private readonly IAddMovieUseCase addMovieUseCase;
        private readonly ICinemaHallApiProxy cinemaHallApiProxy;
        private readonly IFilmApiProxy filmApiProxy;
        private readonly IMovieApiProxy movieApiProxy;
        private readonly IRandomValueProvider randomValueProvider;
        private readonly IMovieFactory movieFactory;

        private readonly IReadOnlyCollection<(int, int, int)> dates = new[]
        {
            (2020, 9, 21),
            (2020, 9, 22),
            (2020, 9, 23),
            (2020, 9, 24),
            (2020, 9, 25),
            (2020, 9, 26),
            (2020, 9, 27)
        };

        private readonly IReadOnlyCollection<(int, int)> times = new[]
        {
            (11, 30),
            (13, 00),
            (15, 30),
            (17, 00),
            (18, 30),
            (20, 00),
            (21, 30)
        };

        public MoviesSeeder(IAddMovieUseCase addMovieUseCase, ICinemaHallApiProxy cinemaHallApiProxy, IFilmApiProxy filmApiProxy, IMovieApiProxy movieApiProxy, IRandomValueProvider randomValueProvider, IMovieFactory movieFactory)
        {
            this.addMovieUseCase = addMovieUseCase;
            this.cinemaHallApiProxy = cinemaHallApiProxy;
            this.filmApiProxy = filmApiProxy;
            this.movieApiProxy = movieApiProxy;
            this.randomValueProvider = randomValueProvider;
            this.movieFactory = movieFactory;
        }

        public async Task SeedAsync()
        {
            IReadOnlyCollection<IMovie> storedMovies = await movieApiProxy.GetAllAsync();
            if (storedMovies.Any())
            {
                return;
            }

            IReadOnlyCollection<ICinemaHall> storedCinemaHalls = await cinemaHallApiProxy.GetAllAsync();
            IReadOnlyCollection<IFilm> storedFilms = await filmApiProxy.GetAllAsync();

            var tasks = new List<Task>();
            for (int i = 0; i < MoviesCount; i++)
            {
                ICinemaHall cinemaHall = storedCinemaHalls.ElementAt(randomValueProvider.GetRandomInt(0, storedCinemaHalls.Count));
                IFilm film = storedFilms.ElementAt(randomValueProvider.GetRandomInt(0, storedFilms.Count));
                (int, int, int) date = dates.ElementAt(randomValueProvider.GetRandomInt(0, dates.Count));
                (int, int) time = times.ElementAt(randomValueProvider.GetRandomInt(0, times.Count));

                var dateTime = new DateTime(date.Item1, date.Item2, date.Item3, time.Item1, time.Item2, 0);
                IMovie movie = movieFactory.Create(new MovieCreationModel(dateTime, film, cinemaHall));
                tasks.Add(addMovieUseCase.AddAsync(movie));
            }

            await Task.WhenAll(tasks);
        }
    }
}