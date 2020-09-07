using System.Threading.Tasks;
using TicketFlow.MovieService.Client.Extensibility.Entities;

namespace TicketFlow.ApiGateway.Service
{
    internal interface IAddMovieUseCase
    {
        public Task AddAsync(IMovie movie);
    }
}