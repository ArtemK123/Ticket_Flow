using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;

namespace TicketFlow.MovieService.Client.Proxies
{
    internal class FilmApiProxy : IFilmApiProxy
    {
        public IReadOnlyCollection<IFilm> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public int Add(FilmCreationModel filmCreationModel)
        {
            throw new System.NotImplementedException();
        }
    }
}