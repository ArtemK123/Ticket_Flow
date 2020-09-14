using System.Collections.Generic;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Models;

namespace TicketFlow.TicketService.Service
{
    public interface ITicketService
    {
        IReadOnlyCollection<ITicket> GetByMovieId(int movieId);

        IReadOnlyCollection<ITicket> GetByUserEmail(string userEmail);

        int Add(TicketCreationModel ticketCreationModel);

        void Order(OrderModel orderModel);
    }
}