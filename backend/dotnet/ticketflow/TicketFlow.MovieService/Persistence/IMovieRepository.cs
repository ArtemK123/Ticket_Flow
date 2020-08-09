using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal interface IMovieRepository : ICrudRepository<int, IMovie>
    {
    }
}