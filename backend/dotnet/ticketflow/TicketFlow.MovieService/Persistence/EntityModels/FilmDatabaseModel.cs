using System;

namespace TicketFlow.MovieService.Persistence.EntityModels
{
    public class FilmDatabaseModel
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