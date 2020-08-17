using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.CinemaHallApi.ClientModels;

namespace TicketFlow.MovieService.WebApi.CinemaHallApi.Controllers
{
    [ApiController]
    [Route("cinema-halls")]
    public class CinemaHallApiController : ControllerBase
    {
        private readonly ICinemaHallService cinemaHallService;

        public CinemaHallApiController(ICinemaHallService cinemaHallService)
        {
            this.cinemaHallService = cinemaHallService;
        }

        [HttpGet]
        public IReadOnlyCollection<ICinemaHall> GetAll()
        {
            return cinemaHallService.GetAll();
        }

        [HttpPost]
        public int Add([FromBody] CinemaHallCreationApiModel cinemaHallCreationApiModel)
        {
            var creationModel = new CinemaHallCreationModel(cinemaHallCreationApiModel.Name, cinemaHallCreationApiModel.Location, cinemaHallCreationApiModel.SeatRows, cinemaHallCreationApiModel.SeatsInRow);
            ICinemaHall createdEntity = cinemaHallService.Add(creationModel);
            return createdEntity.Id;
        }
    }
}