using System.Collections.Generic;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Exceptions;
using TicketFlow.TicketService.Client.Extensibility.Factories;
using TicketFlow.TicketService.Client.Extensibility.Models;
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

        public int Add(TicketCreationModel ticketCreationModel)
        {
            ITicket newTicket = ticketFactory.Create(ticketCreationModel);
            ticketRepository.Add(newTicket);
            return newTicket.Id;
        }

        public void Order(OrderModel orderModel)
        {
            if (!ticketRepository.TryGet(orderModel.TicketId, out ITicket ticket))
            {
                throw new NotFoundException($"Ticket with id={orderModel.TicketId} is not found");
            }

            if (ticket is IOrderedTicket _)
            {
                throw new TicketAlreadyOrderedException($"Ticket with id={orderModel.TicketId} is already ordered");
            }

            IOrderedTicket orderedTicket = ticket.Order(orderModel.BuyerEmail);
            ticketRepository.Update(orderedTicket);
        }
    }
}