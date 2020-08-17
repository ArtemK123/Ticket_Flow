using System.Collections.Generic;
using TicketFlow.Common.Factories;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Exceptions;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.Service
{
    internal class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly ICinemaHallService cinemaHallService;
        private readonly IFilmService filmService;
        private readonly IEntityFactory<IMovie, MovieCreationModel> entityFactory;

        public MovieService(IMovieRepository movieRepository, ICinemaHallService cinemaHallService, IFilmService filmService, IEntityFactory<IMovie, MovieCreationModel> entityFactory)
        {
            this.movieRepository = movieRepository;
            this.cinemaHallService = cinemaHallService;
            this.filmService = filmService;
            this.entityFactory = entityFactory;
        }

        public IReadOnlyCollection<IMovie> GetAll()
        {
            return movieRepository.GetAll();
        }

        public IMovie GetById(int id)
            => movieRepository.TryGet(id, out IMovie movie)
                ? movie
                : throw new NotFoundException($"Movie with id=${id} is not found");

        public IMovie Add(MovieCreationIdReferencedModel creationIdReferencedModel)
        {
            IFilm film = filmService.GetById(creationIdReferencedModel.FilmId);
            ICinemaHall cinemaHall = cinemaHallService.GetById(creationIdReferencedModel.CinemaHallId);
            IMovie createdMovie = entityFactory.Create(new MovieCreationModel(creationIdReferencedModel.StartTime, film, cinemaHall));
            movieRepository.Add(createdMovie);
            return createdMovie;
        }
    }
}