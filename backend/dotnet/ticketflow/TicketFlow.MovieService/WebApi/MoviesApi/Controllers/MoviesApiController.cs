using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.MoviesApi.ClientModels;

namespace TicketFlow.MovieService.WebApi.MoviesApi.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesApiController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public IReadOnlyCollection<IMovie> GetAll()
        {
            return movieService.GetAll();
        }

        [HttpGet("{id}")]
        public IMovie GetById([FromRoute] int id)
        {
            return movieService.GetById(id);
        }

        [HttpPost]
        public int Add([FromBody] MovieCreationIdReferencedApiModel movieCreationIdReferencedApiModel)
        {
            var creationModel = new MovieCreationIdReferencedModel(movieCreationIdReferencedApiModel.StartTime, movieCreationIdReferencedApiModel.FilmId, movieCreationIdReferencedApiModel.CinemaHallId);
            IMovie createdEntity = movieService.Add(creationModel);
            return createdEntity.Id;
        }
    }
}