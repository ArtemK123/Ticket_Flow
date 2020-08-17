using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.MovieApi.ClientModels;

namespace TicketFlow.MovieService.WebApi.MovieApi.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieApiController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MovieApiController(IMovieService movieService)
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