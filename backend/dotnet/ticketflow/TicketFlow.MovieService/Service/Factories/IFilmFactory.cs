using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;

namespace TicketFlow.MovieService.Service.Factories
{
    public interface IFilmFactory
    {
        IFilm Create(FilmCreationModel creationModel);
    }
}