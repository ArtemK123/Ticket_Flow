using System;

namespace TicketFlow.MovieService.Domain.Models.MovieModels
{
    public class MovieCreationIdReferencedModel
    {
        public MovieCreationIdReferencedModel(DateTime startTime, int filmId, int cinemaHallId)
        {
            StartTime = startTime;
            FilmId = filmId;
            CinemaHallId = cinemaHallId;
        }

        public DateTime StartTime { get; }

        public int FilmId { get; }

        public int CinemaHallId { get; }
    }
}