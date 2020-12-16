using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.IntegrationTest.Fakes
{
    internal class FakeMovieRepository : IMovieRepository
    {
        public bool TryGet(int identifier, out IMovie entity)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<IMovie> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(IMovie entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IMovie entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int identifier)
        {
            throw new System.NotImplementedException();
        }
    }
}