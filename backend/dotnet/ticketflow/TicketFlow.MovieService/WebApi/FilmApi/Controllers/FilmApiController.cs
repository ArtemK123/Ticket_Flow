using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.FilmApi.ClientModels;

namespace TicketFlow.MovieService.WebApi.FilmApi.Controllers
{
    [ApiController]
    [Route("films")]
    public class FilmApiController : ControllerBase
    {
        private readonly IFilmService filmService;
        private readonly IFilmSerializer filmSerializer;

        public FilmApiController(IFilmService filmService, IFilmSerializer filmSerializer)
        {
            this.filmService = filmService;
            this.filmSerializer = filmSerializer;
        }

        [HttpGet]
        public IReadOnlyCollection<FilmSerializationModel> GetAll()
        {
            IReadOnlyCollection<IFilm> films = filmService.GetAll();
            return films.Select(filmSerializer.Serialize).ToArray();
        }

        [HttpPost]
        public int Add([FromBody] FilmCreationApiModel filmCreationApiModel)
        {
            var creationModel = new FilmCreationModel(
                filmCreationApiModel.Title,
                filmCreationApiModel.Description,
                filmCreationApiModel.PremiereDate,
                filmCreationApiModel.Creator,
                filmCreationApiModel.Duration,
                filmCreationApiModel.AgeLimit);

            IFilm createdEntity = filmService.Add(creationModel);
            return createdEntity.Id;
        }
    }
}