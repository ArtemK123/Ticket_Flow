using System.Collections.Generic;
using TicketFlow.TicketService.Domain.Entities;

namespace TicketFlow.TicketService.Persistence
{
    internal interface ITicketRepository
    {
        bool TryGetById(int id, out ITicket ticket);

        IReadOnlyCollection<ITicket> GetByMovieId(int movieId);

        IReadOnlyCollection<ITicket> GetByBuyerEmail(string buyerEmail);

        void Add(ITicket ticket);

        void Update(ITicket ticket);
    }
}