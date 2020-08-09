using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal class FilmRepository : IFilmRepository
    {
        public bool TryGetById(int id, out IFilm film)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<IFilm> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(IFilm film)
        {
            throw new System.NotImplementedException();
        }
    }
}