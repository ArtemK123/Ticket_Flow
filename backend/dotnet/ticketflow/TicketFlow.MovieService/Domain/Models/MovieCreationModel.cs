using System;
using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Domain.Models
{
    public class MovieCreationModel
    {
        public MovieCreationModel(DateTime startTime, IFilm film, ICinemaHall cinemaHall)
        {
            StartTime = startTime;
            Film = film;
            CinemaHall = cinemaHall;
        }

        public DateTime StartTime { get; }

        public IFilm Film { get; }

        public ICinemaHall CinemaHall { get; }
    }
}