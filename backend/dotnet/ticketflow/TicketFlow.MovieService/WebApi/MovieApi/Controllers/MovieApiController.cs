using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.MovieApi.ClientModels;

namespace TicketFlow.MovieService.WebApi.MovieApi.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieApiController : ControllerBase
    {
        private readonly IMovieService movieService;
        private readonly IMovieSerializer movieSerializer;

        public MovieApiController(IMovieService movieService, IMovieSerializer movieSerializer)
        {
            this.movieService = movieService;
            this.movieSerializer = movieSerializer;
        }

        [HttpGet]
        public IReadOnlyCollection<MovieSerializationModel> GetAll()
        {
            IReadOnlyCollection<IMovie> movies = movieService.GetAll();
            return movies.Select(movieSerializer.Serialize).ToArray();
        }

        [HttpGet("{id}")]
        public MovieSerializationModel GetById([FromRoute] int id)
        {
            IMovie movie = movieService.GetById(id);
            return movieSerializer.Serialize(movie);
        }

        [HttpPost]
        public int Add([FromBody] MovieCreationIdReferencedApiModel movieCreationIdReferencedApiModel)
        {
            var creationModel = new MovieCreationIdReferencedModel(
                movieCreationIdReferencedApiModel.StartTime,
                movieCreationIdReferencedApiModel.FilmId,
                movieCreationIdReferencedApiModel.CinemaHallId);

            IMovie createdEntity = movieService.Add(creationModel);
            return createdEntity.Id;
        }
    }
}