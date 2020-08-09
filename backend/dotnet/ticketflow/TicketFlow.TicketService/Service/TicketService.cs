using System.Collections.Generic;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Exceptions;
using TicketFlow.TicketService.Domain.Models;
using TicketFlow.TicketService.Persistence;

namespace TicketFlow.TicketService.Service
{
    internal class TicketService : ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        private readonly ITicketFactory ticketFactory;

        public TicketService(ITicketRepository ticketRepository, ITicketFactory ticketFactory)
        {
            this.ticketRepository = ticketRepository;
            this.ticketFactory = ticketFactory;
        }

        public IReadOnlyCollection<ITicket> GetByMovieId(int movieId)
        {
            return ticketRepository.GetByMovieId(movieId);
        }

        public IReadOnlyCollection<ITicket> GetByUserEmail(string userEmail)
        {
            return ticketRepository.GetByBuyerEmail(userEmail);
        }

        public int Add(TicketModelWithoutId ticketModelWithoutId)
        {
            ITicket newTicket = ticketFactory.CreateTicket(ticketModelWithoutId);

            ticketRepository.Add(newTicket);

            return newTicket.Id;
        }

        public void Order(OrderModel orderModel)
        {
            if (!ticketRepository.TryGetById(orderModel.TicketId, out ITicket ticket))
            {
                throw new NotFoundException($"Ticket with id={orderModel.TicketId} is not found");
            }

            if (ticket.IsOrdered)
            {
                throw new TicketAlreadyOrderedException($"Ticket with id={orderModel.TicketId} is already ordered");
            }

            ticket.Order(orderModel.BuyerEmail);
            ticketRepository.Update(ticket);
        }
    }
}