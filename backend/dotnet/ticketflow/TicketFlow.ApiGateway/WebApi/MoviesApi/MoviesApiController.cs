using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.ApiGateway.WebApi.MoviesApi.Models;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Proxies;

namespace TicketFlow.ApiGateway.WebApi.MoviesApi
{
    [ApiController]
    [Route("/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieApiProxy movieApiProxy;

        public MoviesApiController(IMovieApiProxy movieApiProxy)
        {
            this.movieApiProxy = movieApiProxy;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<ShortMovieModel>> GetAllAsync()
        {
            IReadOnlyCollection<IMovie> movies = await movieApiProxy.GetAllAsync();
            return movies.Select(movie => new ShortMovieModel(movie)).ToArray();
        }

        [HttpGet("{id}")]
        public async Task<IMovie> GetByIdAsync([FromRoute] int id)
        {
            return await movieApiProxy.GetByIdAsync(id);
        }
    }
}