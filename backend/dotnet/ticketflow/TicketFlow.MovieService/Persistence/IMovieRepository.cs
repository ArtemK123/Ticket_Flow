using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal interface IMovieRepository
    {
        bool TryGetById(int id, out IMovie movie);

        IReadOnlyCollection<IMovie> GetAll();

        void Add(IMovie movie);
    }
}