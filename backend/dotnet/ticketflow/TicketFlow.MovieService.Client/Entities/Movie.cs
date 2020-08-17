using System;
using TicketFlow.MovieService.Client.Extensibility.Entities;

namespace TicketFlow.MovieService.Client.Entities
{
    internal class Movie : IMovie
    {
        public Movie(int id, DateTime startTime, IFilm film, ICinemaHall cinemaHall)
        {
            Id = id;
            StartTime = startTime;
            Film = film;
            CinemaHall = cinemaHall;
        }

        public int Id { get; }

        public DateTime StartTime { get; }

        public IFilm Film { get; }

        public ICinemaHall CinemaHall { get; }
    }
}