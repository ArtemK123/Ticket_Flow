using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Service;
using TicketFlow.MovieService.WebApi.CinemaHallApi.ClientModels;

namespace TicketFlow.MovieService.WebApi.CinemaHallApi.Controllers
{
    [ApiController]
    [Route("cinema-halls")]
    public class CinemaHallApiController : ControllerBase
    {
        private readonly ICinemaHallService cinemaHallService;
        private readonly ICinemaHallSerializer cinemaHallSerializer;

        public CinemaHallApiController(ICinemaHallService cinemaHallService, ICinemaHallSerializer cinemaHallSerializer)
        {
            this.cinemaHallService = cinemaHallService;
            this.cinemaHallSerializer = cinemaHallSerializer;
        }

        [HttpGet]
        public IReadOnlyCollection<CinemaHallSerializationModel> GetAll()
        {
            IReadOnlyCollection<ICinemaHall> cinemaHalls = cinemaHallService.GetAll();
            return cinemaHalls.Select(cinemaHallSerializer.Serialize).ToArray();
        }

        [HttpPost]
        public int Add([FromBody] CinemaHallCreationApiModel cinemaHallCreationApiModel)
        {
            var creationModel = new CinemaHallCreationModel(
                cinemaHallCreationApiModel.Name,
                cinemaHallCreationApiModel.Location,
                cinemaHallCreationApiModel.SeatRows,
                cinemaHallCreationApiModel.SeatsInRow);

            ICinemaHall createdEntity = cinemaHallService.Add(creationModel);
            return createdEntity.Id;
        }
    }
}