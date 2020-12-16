using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.IntegrationTest.Fakes
{
    internal class FakeFilmRepository : IFilmRepository
    {
        public bool TryGet(int identifier, out IFilm entity)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<IFilm> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(IFilm entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IFilm entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int identifier)
        {
            throw new System.NotImplementedException();
        }
    }
}