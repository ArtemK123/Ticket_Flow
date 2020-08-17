using TicketFlow.Common.Factories;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;

namespace TicketFlow.MovieService.Client.Extensibility.Factories
{
    public interface IMovieFactory : IEntityFactory<IMovie, MovieCreationModel>, IEntityFactory<IMovie, StoredMovieCreationModel>
    {
    }
}