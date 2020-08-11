using System.Collections.Generic;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;

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