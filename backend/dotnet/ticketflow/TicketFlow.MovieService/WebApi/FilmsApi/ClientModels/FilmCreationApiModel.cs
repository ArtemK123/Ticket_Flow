using System;

namespace TicketFlow.MovieService.WebApi.FilmsApi.ClientModels
{
    public class FilmCreationApiModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PremiereDate { get; set; }

        public string Creator { get; set; }

        public int Duration { get; set; }

        public int AgeLimit { get; set; }
    }
}