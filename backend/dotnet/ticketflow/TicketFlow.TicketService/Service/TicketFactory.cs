using TicketFlow.Common.Providers;
using TicketFlow.TicketService.Entities;
using TicketFlow.TicketService.Service.Models;

namespace TicketFlow.TicketService.Service
{
    internal class TicketFactory : ITicketFactory
    {
        private readonly IRandomValueProvider randomValueProvider;

        public TicketFactory(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public ITicket CreateTicket(TicketModelWithoutId ticketModel)
        {
            int ticketId = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new Ticket(ticketId, ticketModel.MovieId, ticketModel.BuyerEmail, ticketModel.Row, ticketModel.Seat, ticketModel.Price);
        }
    }
}