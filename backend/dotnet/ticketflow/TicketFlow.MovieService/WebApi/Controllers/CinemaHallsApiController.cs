using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.ClientModels.Requests;

namespace TicketFlow.MovieService.WebApi.Controllers
{
    [ApiController]
    [Route("cinema-halls")]
    public class CinemaHallsApiController : ControllerBase
    {
        private readonly ICinemaHallService cinemaHallService;

        public CinemaHallsApiController(ICinemaHallService cinemaHallService)
        {
            this.cinemaHallService = cinemaHallService;
        }

        [HttpGet]
        public IReadOnlyCollection<ICinemaHall> GetAll()
        {
            return cinemaHallService.GetAll();
        }

        [HttpPost]
        public int Add([FromBody] AddCinemaHallRequest addCinemaHallRequest)
        {
            var creationModel = new CinemaHallCreationModel(addCinemaHallRequest.Name, addCinemaHallRequest.Location, addCinemaHallRequest.SeatRows, addCinemaHallRequest.SeatsInRow);
            ICinemaHall createdEntity = cinemaHallService.Add(creationModel);
            return createdEntity.Id;
        }
    }
}