using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Entities;

namespace TicketFlow.ApiGateway.Models
{
    public class TicketWithMovie
    {
        public TicketWithMovie(ITicket ticket, IMovie movie)
        {
            Ticket = ticket;
            Movie = movie;
        }

        public ITicket Ticket { get; }

        public IMovie Movie { get; }
    }
}