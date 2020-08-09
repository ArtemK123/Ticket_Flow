using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.ClientModels.Requests;

namespace TicketFlow.MovieService.WebApi.Controllers
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
        public int Add([FromBody] AddFilmRequest addFilmRequest)
        {
            var creationModel = new FilmCreationModel(
                addFilmRequest.Title,
                addFilmRequest.Description,
                addFilmRequest.PremiereDate,
                addFilmRequest.Creator,
                addFilmRequest.Duration,
                addFilmRequest.AgeLimit);
            IFilm createdEntity = filmService.Add(creationModel);
            return createdEntity.Id;
        }
    }
}