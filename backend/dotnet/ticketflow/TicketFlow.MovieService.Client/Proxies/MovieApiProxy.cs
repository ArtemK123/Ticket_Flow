using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;

namespace TicketFlow.MovieService.Client.Proxies
{
    internal class MovieApiProxy : IMovieApiProxy
    {
        public IReadOnlyCollection<IMovie> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IMovie GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Add(MovieCreationIdReferencedModel movieCreationIdReferencedModel)
        {
            throw new System.NotImplementedException();
        }
    }
}