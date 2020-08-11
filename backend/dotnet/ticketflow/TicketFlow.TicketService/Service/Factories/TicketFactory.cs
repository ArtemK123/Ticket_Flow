using System;
using System.Linq;
using TicketFlow.Common.Providers;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;
using TicketFlow.TicketService.Service.Validators;

namespace TicketFlow.TicketService.Service.Factories
{
    internal class TicketFactory : ITicketFactory
    {
        private readonly IRandomValueProvider randomValueProvider;
        private readonly ITicketCreationModelValidator ticketCreationModelValidator;

        public TicketFactory(IRandomValueProvider randomValueProvider, ITicketCreationModelValidator ticketCreationModelValidator)
        {
            this.randomValueProvider = randomValueProvider;
            this.ticketCreationModelValidator = ticketCreationModelValidator;
        }

        public ITicket Create(TicketCreationModel creationModel)
        {
            ValidateCreationModel(creationModel);
            int ticketId = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new Ticket(ticketId, creationModel.MovieId, creationModel.Row, creationModel.Seat, creationModel.Price);
        }

        public ITicket Create(StoredTicketCreationModel creationModel)
            => new Ticket(creationModel.Id, creationModel.MovieId, creationModel.Row, creationModel.Seat, creationModel.Price);

        public IOrderedTicket Create(OrderedTicketCreationModel creationModel)
            => new OrderedTicket(creationModel.Id, creationModel.MovieId, creationModel.Row, creationModel.Seat, creationModel.Price, creationModel.BuyerEmail);

        private void ValidateCreationModel(TicketCreationModel creationModel)
        {
            var validationMessages = ticketCreationModelValidator.Validate(creationModel);
            if (validationMessages.Count > 0)
            {
                throw new ArgumentException(validationMessages.First());
            }
        }
    }
}