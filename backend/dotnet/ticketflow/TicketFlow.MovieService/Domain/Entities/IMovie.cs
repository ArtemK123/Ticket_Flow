using System;

namespace TicketFlow.MovieService.Domain.Entities
{
    public interface IMovie
    {
        public int Id { get; }

        public DateTime StartTime { get; }

        public IFilm Film { get; }

        public ICinemaHall CinemaHall { get; }
    }
}