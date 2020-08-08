using System.Collections.Generic;
using TicketFlow.TicketService.Domain.Entities;

namespace TicketFlow.TicketService.Persistence
{
    internal class TicketRepository : ITicketRepository
    {
        public bool TryGetById(int id, out ITicket ticket)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetByMovie(int movieId, out IReadOnlyCollection<ITicket> tickets)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetByUserEmail(string userEmail, out IReadOnlyCollection<ITicket> tickets)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ITicket ticket)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int ticketId, ITicket ticket)
        {
            throw new System.NotImplementedException();
        }
    }
}