using TicketFlow.Common.Repositories;
using TicketFlow.MovieService.Domain.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal interface ICinemaHallRepository : ICrudRepository<int, ICinemaHall>
    {
    }
}