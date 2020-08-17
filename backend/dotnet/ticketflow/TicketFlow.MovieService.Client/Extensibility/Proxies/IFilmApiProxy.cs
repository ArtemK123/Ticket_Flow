using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;

namespace TicketFlow.MovieService.Client.Extensibility.Proxies
{
    public interface IFilmApiProxy
    {
        public IReadOnlyCollection<IFilm> GetAll();

        public int Add(FilmCreationModel filmCreationModel);
    }
}