using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;

namespace TicketFlow.ApiGateway.Service
{
    public interface IMovieService
    {
        IReadOnlyCollection<IMovie> GetAll();

        IMovie Get(int id);
    }
}