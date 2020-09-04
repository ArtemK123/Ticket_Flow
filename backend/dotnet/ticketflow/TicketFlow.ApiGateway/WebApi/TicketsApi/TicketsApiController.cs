using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.ApiGateway.Models;
using TicketFlow.ApiGateway.Service;
using TicketFlow.ApiGateway.WebApi.TicketsApi.Converters;
using TicketFlow.ApiGateway.WebApi.TicketsApi.Models;
using TicketFlow.Common.Readers;

namespace TicketFlow.ApiGateway.WebApi.TicketsApi
{
    [ApiController]
    [Route("/tickets")]
    public class TicketsApiController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly IStringFromStreamReader stringFromStreamReader;
        private readonly ITicketWithMovieService ticketWithMovieService;
        private readonly ITicketWithMovieConverter ticketWithMovieConverter;

        public TicketsApiController(
            ITicketService ticketService,
            IStringFromStreamReader stringFromStreamReader,
            ITicketWithMovieService ticketWithMovieService,
            ITicketWithMovieConverter ticketWithMovieConverter)
        {
            this.ticketService = ticketService;
            this.stringFromStreamReader = stringFromStreamReader;
            this.ticketWithMovieService = ticketWithMovieService;
            this.ticketWithMovieConverter = ticketWithMovieConverter;
        }

        [HttpGet("by-movie/{movieId}")]
        public IReadOnlyCollection<TicketClientModel> GetTicketsByMovie([FromRoute] int movieId)
        {
            IReadOnlyCollection<TicketWithMovie> ticketsWithMovie = ticketWithMovieService.GetByMovieId(movieId);
            return ticketsWithMovie.Select(ticketWithMovieConverter.Convert).ToArray();
        }

        [HttpPost("by-user")]
        public async Task<IReadOnlyCollection<TicketClientModel>> GetTicketsByUserAsync()
        {
            string token = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            IReadOnlyCollection<TicketWithMovie> ticketsWithMovie = ticketWithMovieService.GetByToken(token);
            return ticketsWithMovie.Select(ticketWithMovieConverter.Convert).ToArray();
        }

        [HttpPost("order")]
        public string Order([FromBody] TicketOrderRequest ticketOrderRequest)
        {
            ticketService.Order(ticketOrderRequest.TicketId, ticketOrderRequest.Token);
            return "Ordered successfully";
        }
    }
}