using TicketFlow.TicketService.Entities;
using TicketFlow.TicketService.Service.Models;

namespace TicketFlow.TicketService.Service
{
    public interface ITicketFactory
    {
        ITicket CreateTicket(TicketModelWithoutId ticketModel);
    }
}