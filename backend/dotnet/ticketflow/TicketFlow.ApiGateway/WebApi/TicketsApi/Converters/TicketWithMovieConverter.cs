using TicketFlow.ApiGateway.Models;
using TicketFlow.ApiGateway.WebApi.TicketsApi.Models;
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Serializers;

namespace TicketFlow.ApiGateway.WebApi.TicketsApi.Converters
{
    internal class TicketWithMovieConverter : ITicketWithMovieConverter
    {
        private readonly ITicketSerializer ticketSerializer;

        public TicketWithMovieConverter(ITicketSerializer ticketSerializer)
        {
            this.ticketSerializer = ticketSerializer;
        }

        public TicketClientModel Convert(TicketWithMovie ticketWithMovie)
        {
            TicketSerializationModel ticketSerializationModel = ticketSerializer.Serialize(ticketWithMovie.Ticket);
            return new TicketClientModel(ticketSerializationModel, ticketWithMovie.Movie);
        }
    }
}