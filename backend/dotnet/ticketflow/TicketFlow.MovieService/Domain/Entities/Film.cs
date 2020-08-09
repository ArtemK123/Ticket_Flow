using System;

namespace TicketFlow.MovieService.Domain.Entities
{
    internal class Film : IFilm
    {
        public Film(int id, string title, string description, DateTime premiereDate, string creator, int duration, int ageLimit)
        {
            Id = id;
            Title = title;
            Description = description;
            PremiereDate = premiereDate;
            Creator = creator;
            Duration = duration;
            AgeLimit = ageLimit;
        }

        public int Id { get; }

        public string Title { get; }

        public string Description { get; }

        public DateTime PremiereDate { get; }

        public string Creator { get; }

        public int Duration { get; }

        public int AgeLimit { get; }
    }
}