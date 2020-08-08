using System;
using TicketFlow.Common.Providers;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;

namespace TicketFlow.TicketService.Service
{
    internal class TicketFactory : ITicketFactory
    {
        private const string ShouldBePossibleExceptionMessage = "{0} should be positive";
        private readonly IRandomValueProvider randomValueProvider;

        public TicketFactory(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public ITicket CreateTicket(TicketModelWithoutId ticketModel)
        {
            ValidateTicketModel(ticketModel);
            int ticketId = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new Ticket(ticketId, ticketModel.MovieId, ticketModel.BuyerEmail, ticketModel.Row, ticketModel.Seat, ticketModel.Price);
        }

        private static void ValidateTicketModel(TicketModelWithoutId ticketModel)
        {
            if (ticketModel.Row < 0)
            {
                throw new ArgumentException(string.Format(ShouldBePossibleExceptionMessage, "Row number"));
            }

            if (ticketModel.Seat < 0)
            {
                throw new ArgumentException(string.Format(ShouldBePossibleExceptionMessage, "Seat number"));
            }

            if (ticketModel.Price < 0)
            {
                throw new ArgumentException(string.Format(ShouldBePossibleExceptionMessage, "Price"));
            }
        }
    }
}