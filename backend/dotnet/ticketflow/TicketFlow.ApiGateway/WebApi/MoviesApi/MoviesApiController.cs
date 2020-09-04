using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.ApiGateway.Service;
using TicketFlow.ApiGateway.WebApi.MoviesApi.Models;
using TicketFlow.MovieService.Client.Extensibility.Entities;

namespace TicketFlow.ApiGateway.WebApi.MoviesApi
{
    [ApiController]
    [Route("/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesApiController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public IReadOnlyCollection<ShortMovieModel> GetAll()
            => movieService.GetAll().Select(movie => new ShortMovieModel(movie)).ToArray();

        [HttpGet("{id}")]
        public IMovie GetById([FromRoute] int id)
            => movieService.Get(id);
    }
}