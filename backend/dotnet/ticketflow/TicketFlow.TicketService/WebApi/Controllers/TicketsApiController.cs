using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Extractors;
using TicketFlow.TicketService.Entities;
using TicketFlow.TicketService.Service;
using TicketFlow.TicketService.Service.Models;
using TicketFlow.TicketService.WebApi.ClientModels.Requests;

namespace TicketFlow.TicketService.WebApi.Controllers
{
    [ApiController]
    [Route("/tickets")]
    public class TicketsApiController : ControllerBase
    {
        private readonly ITicketsService ticketsService;
        private readonly IStringFromStreamReader stringFromStreamReader;

        public TicketsApiController(ITicketsService ticketsService, IStringFromStreamReader stringFromStreamReader)
        {
            this.ticketsService = ticketsService;
            this.stringFromStreamReader = stringFromStreamReader;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return new OkObjectResult("Hello from Ticket service API");
        }

        [HttpGet("by-movie/{movieId}")]
        public IReadOnlyCollection<ITicket> GetByMovieId([FromRoute] int movieId)
        {
            return ticketsService.GetByMovieId(movieId);
        }

        [HttpPost("by-user")]
        public async Task<IReadOnlyCollection<ITicket>> GetByUserEmail()
        {
            string userEmail = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            return ticketsService.GetByUserEmail(userEmail);
        }

        [HttpPost("")]
        public int Add([FromBody] AddTicketRequestModel addTicketRequestModel)
        {
            int createdEntityId = ticketsService.Add(Convert(addTicketRequestModel));
            return createdEntityId;
        }

        [HttpPost("order")]
        public string Order([FromBody] OrderRequestModel orderRequestModel)
        {
            var orderModel = new OrderModel(orderRequestModel.TicketId, orderRequestModel.BuyerEmail);
            ticketsService.Order(orderModel);

            return "Ordered successfully";
        }

        private TicketModelWithoutId Convert(AddTicketRequestModel requestModel)
            => new TicketModelWithoutId(requestModel.MovieId, requestModel.BuyerEmail, requestModel.Row, requestModel.Seat, requestModel.Price);
    }
}