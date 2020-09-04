using System.Collections.Generic;
using TicketFlow.TicketService.Client.Extensibility.Entities;

namespace TicketFlow.ApiGateway.Service
{
    public interface ITicketService
    {
        IReadOnlyCollection<ITicket> GetByMovieId(int movieId);

        void Order(int ticketId, string token);
    }
}