using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal interface IFilmRepository : ICrudRepository<int, IFilm>
    {
    }
}