using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;
using TicketFlow.TicketService.Service.Factories;

namespace TicketFlow.TicketService.Service.Serializers
{
    internal class TicketSerializer : ITicketSerializer
    {
        private readonly ITicketFactory ticketFactory;

        public TicketSerializer(ITicketFactory ticketFactory)
        {
            this.ticketFactory = ticketFactory;
        }

        public TicketSerializationModel Serialize(ITicket ticket)
            => new TicketSerializationModel
            {
                Id = ticket.Id,
                MovieId = ticket.MovieId,
                Row = ticket.Row,
                Seat = ticket.Seat,
                Price = ticket.Price,
                BuyerEmail = ticket is IOrderedTicket orderedTicket ? orderedTicket.BuyerEmail : null
            };

        public ITicket Deserialize(TicketSerializationModel serializationModel)
        {
            if (!string.IsNullOrEmpty(serializationModel.BuyerEmail))
            {
                return ticketFactory.Create(
                    new OrderedTicketCreationModel(
                        serializationModel.Id,
                        serializationModel.MovieId,
                        serializationModel.Row,
                        serializationModel.Seat,
                        serializationModel.Price,
                        serializationModel.BuyerEmail));
            }

            return ticketFactory.Create(
                    new StoredTicketCreationModel(
                        serializationModel.Id,
                        serializationModel.MovieId,
                        serializationModel.Row,
                        serializationModel.Seat,
                        serializationModel.Price));
        }
    }
}