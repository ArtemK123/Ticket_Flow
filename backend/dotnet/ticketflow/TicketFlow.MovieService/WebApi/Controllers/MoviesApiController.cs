using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.ClientModels.Requests;

namespace TicketFlow.MovieService.WebApi.Controllers
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
        public int Add([FromBody] AddMovieRequestModel addMovieRequestModel)
        {
            var creationModel = new MovieCreationModel(addMovieRequestModel.StartTime, addMovieRequestModel.FilmId, addMovieRequestModel.CinemaHallId);
            IMovie createdEntity = movieService.Add(creationModel);
            return createdEntity.Id;
        }
    }
}