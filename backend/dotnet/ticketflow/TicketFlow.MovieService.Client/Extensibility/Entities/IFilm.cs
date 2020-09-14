using System;

namespace TicketFlow.MovieService.Client.Extensibility.Entities
{
    public interface IFilm
    {
        public int Id { get; }

        public string Title { get; }

        public string Description { get; }

        public DateTime PremiereDate { get; }

        public string Creator { get; }

        public int Duration { get; }

        public int AgeLimit { get; }
    }
}