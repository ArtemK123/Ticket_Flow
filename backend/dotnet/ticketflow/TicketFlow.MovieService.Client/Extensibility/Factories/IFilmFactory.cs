using TicketFlow.Common.Factories;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;

namespace TicketFlow.MovieService.Client.Extensibility.Factories
{
    public interface IFilmFactory : IEntityFactory<IFilm, FilmCreationModel>, IEntityFactory<IFilm, StoredFilmCreationModel>
    {
    }
}