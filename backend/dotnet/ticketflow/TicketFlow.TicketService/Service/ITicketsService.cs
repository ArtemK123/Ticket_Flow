using System.Collections.Generic;
using TicketFlow.TicketService.Entities;
using TicketFlow.TicketService.Service.Models;

namespace TicketFlow.TicketService.Service
{
    public interface ITicketsService
    {
        IReadOnlyCollection<ITicket> GetByMovieId(int movieId);

        IReadOnlyCollection<ITicket> GetByUserEmail(string userEmail);

        int Add(TicketModelWithoutId ticketModelWithoutId);

        void Order(OrderModel orderModel);
    }
}