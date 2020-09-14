using System;

namespace TicketFlow.MovieService.Client.Extensibility.Models.FilmModels
{
    public class FilmSerializationModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PremiereDate { get; set; }

        public string Creator { get; set; }

        public int Duration { get; set; }

        public int AgeLimit { get; set; }
    }
}