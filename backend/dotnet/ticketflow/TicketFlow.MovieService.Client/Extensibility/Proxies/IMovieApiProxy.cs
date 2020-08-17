using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;

namespace TicketFlow.MovieService.Client.Extensibility.Proxies
{
    public interface IMovieApiProxy
    {
        public Task<IReadOnlyCollection<IMovie>> GetAllAsync();

        public Task<IMovie> GetByIdAsync([FromRoute] int id);

        public Task<int> AddAsync(MovieCreationIdReferencedModel movieCreationIdReferencedModel);
    }
}