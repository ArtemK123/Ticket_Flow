using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Extractors;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;
using TicketFlow.TicketService.Service;
using TicketFlow.TicketService.WebApi.ClientModels.Requests;

namespace TicketFlow.TicketService.WebApi.Controllers
{
    [ApiController]
    [Route("/tickets")]
    public class TicketsApiController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly IStringFromStreamReader stringFromStreamReader;

        public TicketsApiController(ITicketService ticketService, IStringFromStreamReader stringFromStreamReader)
        {
            this.ticketService = ticketService;
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
            return ticketService.GetByMovieId(movieId);
        }

        [HttpPost("by-user")]
        public async Task<IReadOnlyCollection<ITicket>> GetByUserEmail()
        {
            string userEmail = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            return ticketService.GetByUserEmail(userEmail);
        }

        [HttpPost("")]
        public int Add([FromBody] AddTicketRequestModel addTicketRequestModel)
        {
            int createdEntityId = ticketService.Add(Convert(addTicketRequestModel));
            return createdEntityId;
        }

        [HttpPost("order")]
        public string Order([FromBody] OrderRequestModel orderRequestModel)
        {
            var orderModel = new OrderModel(orderRequestModel.TicketId, orderRequestModel.BuyerEmail);
            ticketService.Order(orderModel);

            return "Ordered successfully";
        }

        private static TicketModelWithoutId Convert(AddTicketRequestModel requestModel)
            => new TicketModelWithoutId(requestModel.MovieId, requestModel.BuyerEmail, requestModel.Row, requestModel.Seat, requestModel.Price);
    }
}