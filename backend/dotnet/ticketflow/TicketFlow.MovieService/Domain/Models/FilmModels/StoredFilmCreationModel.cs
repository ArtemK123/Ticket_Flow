using System;

namespace TicketFlow.MovieService.Domain.Models.FilmModels
{
    public class StoredFilmCreationModel : FilmCreationModel
    {
        public StoredFilmCreationModel(int id, string title, string description, DateTime premiereDate, string creator, int duration, int ageLimit)
            : base(title, description, premiereDate, creator, duration, ageLimit)
        {
            Id = id;
        }

        public int Id { get; }
    }
}