using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;

namespace TicketFlow.MovieService.Service
{
    public interface IFilmService
    {
        IReadOnlyCollection<IFilm> GetAll();

        IFilm GetById(int id);

        IFilm Add(FilmCreationModel creationModel);
    }
}