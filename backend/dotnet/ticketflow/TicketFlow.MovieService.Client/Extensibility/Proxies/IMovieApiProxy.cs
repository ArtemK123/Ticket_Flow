using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;

namespace TicketFlow.MovieService.Client.Extensibility.Proxies
{
    public interface IMovieApiProxy
    {
        public IReadOnlyCollection<IMovie> GetAll();

        public IMovie GetById([FromRoute] int id);

        public int Add(MovieCreationIdReferencedModel movieCreationIdReferencedModel);
    }
}