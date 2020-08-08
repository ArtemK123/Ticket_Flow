using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;

namespace TicketFlow.TicketService.Service
{
    public interface ITicketFactory
    {
        ITicket CreateTicket(TicketModelWithoutId ticketModel);
    }
}