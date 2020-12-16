using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Exceptions;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using Xunit;

namespace TicketFlow.MovieService.IntegrationTest.ApiTests
{
    public class MovieApiViaProxyTest : IDisposable
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly IMovieApiProxy movieApiProxy;
        private readonly ICinemaHallApiProxy cinemaHallApiProxy;
        private readonly IFilmApiProxy filmApiProxy;

        private CinemaHallCreationModel cinemaHallCreationModel;
        private FilmCreationModel filmCreationModel;

        private int cinemaHallId;
        private int filmId;

        public MovieApiViaProxyTest()
        {
            webApplicationFactory = new WebApplicationFactoryProvider().GetWebApplicationFactory();
            movieApiProxy = webApplicationFactory.Services.GetService<IMovieApiProxy>();
            cinemaHallApiProxy = webApplicationFactory.Services.GetService<ICinemaHallApiProxy>();
            filmApiProxy = webApplicationFactory.Services.GetService<IFilmApiProxy>();

            SetupInitialData().Wait();
        }

        public void Dispose()
        {
            webApplicationFactory?.Dispose();
        }

        [Fact]
        public async Task Add_ThenGetAll_ShouldReturnMovies()
        {
            IReadOnlyCollection<MovieCreationIdReferencedModel> movieCreationModels = new[]
            {
                new MovieCreationIdReferencedModel(new DateTime(2020, 1, 1), filmId, cinemaHallId),
                new MovieCreationIdReferencedModel(new DateTime(2020, 2, 2), filmId, cinemaHallId),
                new MovieCreationIdReferencedModel(new DateTime(2020, 3, 3), filmId, cinemaHallId)
            };

            List<int> addedMovieIds = new List<int>();

            foreach (MovieCreationIdReferencedModel creationModel in movieCreationModels)
            {
                int id = await movieApiProxy.AddAsync(creationModel);
                addedMovieIds.Add(id);
            }

            IReadOnlyCollection<IMovie> fetchedMovies = await movieApiProxy.GetAllAsync();

            Assert.True(fetchedMovies.All(movie => movieCreationModels.Any(creationModel => SameMovie(movie, creationModel))));
            Assert.True(fetchedMovies.All(movie => addedMovieIds.Contains(movie.Id)));
        }

        [Fact]
        public async Task Add_FilmNotFound_ShouldThrowFilmNotFoundByIdException()
        {
            MovieCreationIdReferencedModel creationModel = new MovieCreationIdReferencedModel(new DateTime(2020, 1, 1), default, cinemaHallId);

            FilmNotFoundByIdException exception = await Assert.ThrowsAsync<FilmNotFoundByIdException>(async () => await movieApiProxy.AddAsync(creationModel));
            Assert.Equal($"Film with id=${creationModel.FilmId} is not found", exception.Message);
        }

        [Fact]
        public async Task Add_CinemaHallNotFound_ShouldThrowCinemaHallNotFoundByIdException()
        {
            MovieCreationIdReferencedModel creationModel = new MovieCreationIdReferencedModel(new DateTime(2020, 1, 1), filmId, default);

            CinemaHallNotFoundByIdException exception = await Assert.ThrowsAsync<CinemaHallNotFoundByIdException>(async () => await movieApiProxy.AddAsync(creationModel));
            Assert.Equal($"Cinema hall with id=${creationModel.CinemaHallId} is not found", exception.Message);
        }

        [Fact]
        public async Task GetById_Found_ShouldReturnMovieWithGivenId()
        {
            MovieCreationIdReferencedModel creationModel = new MovieCreationIdReferencedModel(new DateTime(2020, 1, 1), filmId, cinemaHallId);
            int addedMovieId = await movieApiProxy.AddAsync(creationModel);

            IMovie movie = await movieApiProxy.GetByIdAsync(addedMovieId);
            Assert.True(SameMovie(movie, creationModel));
        }

        [Fact]
        public async Task GetById_NotFound_ShouldThrowMovieNotFoundByIdException()
        {
            const int invalidMovieId = default;

            MovieNotFoundByIdException exception = await Assert.ThrowsAsync<MovieNotFoundByIdException>(async () => await movieApiProxy.GetByIdAsync(invalidMovieId));
            Assert.Equal($"Movie with id=${invalidMovieId} is not found", exception.Message);
        }

        private static bool SameMovie(IMovie movie, MovieCreationIdReferencedModel creationModel)
            => movie.StartTime == creationModel.StartTime
               && movie.Film.Id == creationModel.FilmId
               && movie.CinemaHall.Id == creationModel.CinemaHallId;

        private async Task SetupInitialData()
        {
            cinemaHallCreationModel = new CinemaHallCreationModel("Super Cinema", "Kyiv", 5, 6);
            filmCreationModel = new FilmCreationModel("Title1", "Description1", new DateTime(1, 1, 1), "Creator1", 1, 1);

            cinemaHallId = await cinemaHallApiProxy.AddAsync(cinemaHallCreationModel);
            filmId = await filmApiProxy.AddAsync(filmCreationModel);
        }
    }
}