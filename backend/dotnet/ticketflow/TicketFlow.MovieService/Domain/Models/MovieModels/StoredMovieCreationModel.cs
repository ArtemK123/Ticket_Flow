using System;
using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Domain.Models.MovieModels
{
    public class StoredMovieCreationModel : MovieCreationModel
    {
        public StoredMovieCreationModel(int id, DateTime startTime, IFilm film, ICinemaHall cinemaHall)
            : base(startTime, film, cinemaHall)
        {
            Id = id;
        }

        public int Id { get; }
    }
}