using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Readers;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;
using TicketFlow.TicketService.Service;
using TicketFlow.TicketService.Service.Serializers;
using TicketFlow.TicketService.WebApi.ClientModels.Requests;

namespace TicketFlow.TicketService.WebApi.Controllers
{
    [ApiController]
    [Route("/tickets")]
    public class TicketsApiController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly IStringFromStreamReader stringFromStreamReader;
        private readonly ITicketSerializer ticketSerializer;

        public TicketsApiController(ITicketService ticketService, IStringFromStreamReader stringFromStreamReader, ITicketSerializer ticketSerializer)
        {
            this.ticketService = ticketService;
            this.stringFromStreamReader = stringFromStreamReader;
            this.ticketSerializer = ticketSerializer;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return new OkObjectResult("Hello from Ticket service API");
        }

        [HttpGet("by-movie/{movieId}")]
        public IReadOnlyCollection<TicketSerializationModel> GetByMovieId([FromRoute] int movieId)
        {
            IReadOnlyCollection<ITicket> tickets = ticketService.GetByMovieId(movieId);
            return tickets.Select(ticketSerializer.Serialize).ToArray();
        }

        [HttpPost("by-user")]
        public async Task<IReadOnlyCollection<TicketSerializationModel>> GetByUserEmail()
        {
            string userEmail = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            IReadOnlyCollection<ITicket> tickets = ticketService.GetByUserEmail(userEmail);
            return tickets.Select(ticketSerializer.Serialize).ToArray();
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

        private static TicketCreationModel Convert(AddTicketRequestModel requestModel)
            => new TicketCreationModel(requestModel.MovieId, requestModel.Row, requestModel.Seat, requestModel.Price);
    }
}