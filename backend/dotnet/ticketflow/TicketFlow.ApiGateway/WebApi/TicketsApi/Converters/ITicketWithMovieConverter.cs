using TicketFlow.ApiGateway.Models;
using TicketFlow.ApiGateway.WebApi.TicketsApi.Models;

namespace TicketFlow.ApiGateway.WebApi.TicketsApi.Converters
{
    public interface ITicketWithMovieConverter
    {
        TicketClientModel Convert(TicketWithMovie ticketWithMovie);
    }
}