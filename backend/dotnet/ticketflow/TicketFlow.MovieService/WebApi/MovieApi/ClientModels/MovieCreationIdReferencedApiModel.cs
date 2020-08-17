using System;

namespace TicketFlow.MovieService.WebApi.MovieApi.ClientModels
{
    public class MovieCreationIdReferencedApiModel
    {
        public DateTime StartTime { get; set; }

        public int FilmId { get; set; }

        public int CinemaHallId { get; set; }
    }
}