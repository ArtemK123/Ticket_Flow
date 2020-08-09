using System;

namespace TicketFlow.MovieService.WebApi.ClientModels.Requests
{
    public class AddMovieRequestModel
    {
        public DateTime StartTime { get; set; }

        public int FilmId { get; set; }

        public int CinemaHallId { get; set; }
    }
}