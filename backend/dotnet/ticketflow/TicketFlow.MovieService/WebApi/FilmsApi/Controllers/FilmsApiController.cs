using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.FilmsApi.ClientModels;

namespace TicketFlow.MovieService.WebApi.FilmsApi.Controllers
{
    [ApiController]
    [Route("films")]
    public class FilmsApiController : ControllerBase
    {
        private readonly IFilmService filmService;

        public FilmsApiController(IFilmService filmService)
        {
            this.filmService = filmService;
        }

        [HttpGet]
        public IReadOnlyCollection<IFilm> GetAll()
        {
            return filmService.GetAll();
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