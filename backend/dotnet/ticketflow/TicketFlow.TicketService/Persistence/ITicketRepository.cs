using System.Collections.Generic;
using TicketFlow.Common.Repositories;
using TicketFlow.TicketService.Client.Extensibility.Entities;

namespace TicketFlow.TicketService.Persistence
{
    internal interface ITicketRepository : ICrudRepository<int, ITicket>
    {
        IReadOnlyCollection<ITicket> GetByMovieId(int movieId);

        IReadOnlyCollection<ITicket> GetByBuyerEmail(string buyerEmail);
    }
}