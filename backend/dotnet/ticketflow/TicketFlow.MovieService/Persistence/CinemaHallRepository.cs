using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal class CinemaHallRepository : ICinemaHallRepository
    {
        public bool TryGetById(int id, out ICinemaHall cinemaHall)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<ICinemaHall> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(ICinemaHall cinemaHall)
        {
            throw new System.NotImplementedException();
        }
    }
}