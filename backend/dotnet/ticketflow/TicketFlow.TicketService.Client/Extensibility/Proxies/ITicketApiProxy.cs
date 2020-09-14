using System.Collections.Generic;
using System.Threading.Tasks;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Models;

namespace TicketFlow.TicketService.Client.Extensibility.Proxies
{
    public interface ITicketApiProxy
    {
        Task<IReadOnlyCollection<ITicket>> GetByMovieIdAsync(int movieId);

        Task<IReadOnlyCollection<ITicket>> GetByUserEmailAsync(string userEmail);

        Task<int> AddAsync(TicketCreationModel ticketCreationModel);

        Task OrderAsync(OrderModel orderModel);
    }
}