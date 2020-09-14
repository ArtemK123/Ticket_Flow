using System;

namespace TicketFlow.MovieService.Persistence.EntityModels
{
    public class MovieDatabaseModel
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public int FilmId { get; set; }

        public int CinemaHallId { get; set; }
    }
}