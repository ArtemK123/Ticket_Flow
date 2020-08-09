using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Exceptions;
using TicketFlow.MovieService.Domain.Models;
using TicketFlow.MovieService.Persistence;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService.Service
{
    internal class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly ICinemaHallService cinemaHallService;
        private readonly IFilmService filmService;
        private readonly IMovieFactory movieFactory;

        public MovieService(IMovieRepository movieRepository, ICinemaHallService cinemaHallService, IFilmService filmService, IMovieFactory movieFactory)
        {
            this.movieRepository = movieRepository;
            this.cinemaHallService = cinemaHallService;
            this.filmService = filmService;
            this.movieFactory = movieFactory;
        }

        public IReadOnlyCollection<IMovie> GetAll()
        {
            return movieRepository.GetAll();
        }

        public IMovie GetById(int id)
            => movieRepository.TryGetById(id, out IMovie movie)
                ? movie
                : throw new NotFoundException($"Movie with id=${id} is not found");

        public IMovie Add(MovieCreationIdReferencedModel creationIdReferencedModel)
        {
            IFilm film = filmService.GetById(creationIdReferencedModel.FilmId);
            ICinemaHall cinemaHall = cinemaHallService.GetById(creationIdReferencedModel.CinemaHallId);
            IMovie createdMovie = movieFactory.Create(new MovieCreationModel(creationIdReferencedModel.StartTime, film, cinemaHall));
            movieRepository.Add(createdMovie);
            return createdMovie;
        }
    }
}