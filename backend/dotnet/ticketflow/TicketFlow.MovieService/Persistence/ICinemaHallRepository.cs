using TicketFlow.Common.Repositories;
using TicketFlow.MovieService.Client.Extensibility.Entities;

namespace TicketFlow.MovieService.Persistence
{
    internal interface ICinemaHallRepository : ICrudRepository<int, ICinemaHall>
    {
    }
}