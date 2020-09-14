using System.Collections.Generic;
using System.Threading.Tasks;
using TicketFlow.ApiGateway.Models;

namespace TicketFlow.ApiGateway.Service
{
    public interface ITicketWithMovieService
    {
        Task<IReadOnlyCollection<TicketWithMovie>> GetByMovieIdAsync(int movieId);

        Task<IReadOnlyCollection<TicketWithMovie>> GetByTokenAsync(string token);
    }
}