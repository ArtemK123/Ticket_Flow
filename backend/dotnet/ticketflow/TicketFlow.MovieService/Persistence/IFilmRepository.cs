using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal interface IFilmRepository
    {
        bool TryGetById(int id, out IFilm film);

        IReadOnlyCollection<IFilm> GetAll();

        void Add(IFilm film);
    }
}