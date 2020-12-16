using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.IntegrationTest.Fakes
{
    internal class FakeCinemaHallRepository : ICinemaHallRepository
    {
        public bool TryGet(int identifier, out ICinemaHall entity)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<ICinemaHall> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(ICinemaHall entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ICinemaHall entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int identifier)
        {
            throw new System.NotImplementedException();
        }
    }
}