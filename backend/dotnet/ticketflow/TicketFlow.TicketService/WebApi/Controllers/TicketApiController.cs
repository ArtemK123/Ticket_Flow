using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Readers;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Serializers;
using TicketFlow.TicketService.Service;
using TicketFlow.TicketService.WebApi.ClientModels;

namespace TicketFlow.TicketService.WebApi.Controllers
{
    [ApiController]
    [Route("/tickets")]
    public class TicketApiController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly IStringFromStreamReader stringFromStreamReader;
        private readonly ITicketSerializer ticketSerializer;

        public TicketApiController(ITicketService ticketService, IStringFromStreamReader stringFromStreamReader, ITicketSerializer ticketSerializer)
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
        public int Add([FromBody] TicketCreationApiModel ticketCreationApiModel)
        {
            int createdEntityId = ticketService.Add(Convert(ticketCreationApiModel));
            return createdEntityId;
        }

        [HttpPost("order")]
        public string Order([FromBody] OrderApiModel orderApiModel)
        {
            var orderModel = new OrderModel(orderApiModel.TicketId, orderApiModel.BuyerEmail);
            ticketService.Order(orderModel);

            return "Ordered successfully";
        }

        private static TicketCreationModel Convert(TicketCreationApiModel creationApiModel)
            => new TicketCreationModel(creationApiModel.MovieId, creationApiModel.Row, creationApiModel.Seat, creationApiModel.Price);
    }
}