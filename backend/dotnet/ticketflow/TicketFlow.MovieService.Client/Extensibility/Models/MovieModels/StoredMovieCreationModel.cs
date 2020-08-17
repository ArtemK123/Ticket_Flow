using System;
using TicketFlow.MovieService.Client.Extensibility.Entities;

namespace TicketFlow.MovieService.Client.Extensibility.Models.MovieModels
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