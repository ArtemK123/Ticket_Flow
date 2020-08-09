﻿using System;

namespace TicketFlow.MovieService.Domain.Models
{
    public class FilmCreationModel
    {
        public FilmCreationModel(string title, string description, DateTime premiereDate, string creator, int duration, int ageLimit)
        {
            Title = title;
            Description = description;
            PremiereDate = premiereDate;
            Creator = creator;
            Duration = duration;
            AgeLimit = ageLimit;
        }

        public string Title { get; }

        public string Description { get; }

        public DateTime PremiereDate { get; }

        public string Creator { get; }

        public int Duration { get; }

        public int AgeLimit { get; }
    }
}