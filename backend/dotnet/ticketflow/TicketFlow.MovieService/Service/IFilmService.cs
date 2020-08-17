using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;

namespace TicketFlow.MovieService.Service
{
    public interface IFilmService
    {
        IReadOnlyCollection<IFilm> GetAll();

        IFilm GetById(int id);

        IFilm Add(FilmCreationModel creationModel);
    }
}