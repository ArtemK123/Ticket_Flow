using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal class MovieRepository : IMovieRepository
    {
        public bool TryGetById(int id, out IMovie movie)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<IMovie> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(IMovie movie)
        {
            throw new System.NotImplementedException();
        }
    }
}