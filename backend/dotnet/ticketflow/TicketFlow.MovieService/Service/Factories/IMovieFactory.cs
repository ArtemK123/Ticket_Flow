using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;

namespace TicketFlow.MovieService.Service.Factories
{
    public interface IMovieFactory
    {
        IMovie Create(MovieCreationModel creationModel);
    }
}