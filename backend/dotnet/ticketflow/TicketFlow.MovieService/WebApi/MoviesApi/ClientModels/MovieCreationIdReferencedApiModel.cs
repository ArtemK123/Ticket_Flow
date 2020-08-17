using System;

namespace TicketFlow.MovieService.WebApi.MoviesApi.ClientModels
{
    public class MovieCreationIdReferencedApiModel
    {
        public DateTime StartTime { get; set; }

        public int FilmId { get; set; }

        public int CinemaHallId { get; set; }
    }
}