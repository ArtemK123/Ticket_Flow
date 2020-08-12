using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Factories;
using TicketFlow.TicketService.Client.Extensibility.Models;

namespace TicketFlow.TicketService.Client.Extensibility.Serializers
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