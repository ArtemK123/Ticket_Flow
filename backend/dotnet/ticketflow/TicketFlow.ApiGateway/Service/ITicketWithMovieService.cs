using System.Collections.Generic;
using TicketFlow.ApiGateway.Models;

namespace TicketFlow.ApiGateway.Service
{
    public interface ITicketWithMovieService
    {
        IReadOnlyCollection<TicketWithMovie> GetByMovieId(int movieId);

        IReadOnlyCollection<TicketWithMovie> GetByToken(string token);
    }
}