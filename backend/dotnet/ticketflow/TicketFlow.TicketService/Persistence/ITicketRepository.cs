using System.Collections.Generic;
using TicketFlow.TicketService.Domain.Entities;

namespace TicketFlow.TicketService.Persistence
{
    internal interface ITicketRepository
    {
        bool TryGetById(int id, out ITicket ticket);

        bool TryGetByMovie(int movieId, out IReadOnlyCollection<ITicket> tickets);

        bool TryGetByUserEmail(string userEmail, out IReadOnlyCollection<ITicket> tickets);

        void Add(ITicket ticket);

        void Update(int ticketId, ITicket ticket);
    }
}