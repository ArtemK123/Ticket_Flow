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
        private readonly ITicketWithMovieService ticketWithMovieService;
        private readonly IOrderTicketUseCase orderTicketUseCase;
        private readonly IStringFromStreamReader stringFromStreamReader;
        private readonly ITicketWithMovieConverter ticketWithMovieConverter;

        public TicketsApiController(
            ITicketWithMovieService ticketWithMovieService,
            IOrderTicketUseCase orderTicketUseCase,
            IStringFromStreamReader stringFromStreamReader,
            ITicketWithMovieConverter ticketWithMovieConverter)
        {
            this.ticketWithMovieService = ticketWithMovieService;
            this.orderTicketUseCase = orderTicketUseCase;
            this.stringFromStreamReader = stringFromStreamReader;
            this.ticketWithMovieConverter = ticketWithMovieConverter;
        }

        [HttpGet("by-movie/{movieId}")]
        public async Task<IReadOnlyCollection<TicketClientModel>> GetTicketsByMovieAsync([FromRoute] int movieId)
        {
            IReadOnlyCollection<TicketWithMovie> ticketsWithMovie = await ticketWithMovieService.GetByMovieIdAsync(movieId);
            return ticketsWithMovie.Select(ticketWithMovieConverter.Convert).ToArray();
        }

        [HttpPost("by-user")]
        public async Task<IReadOnlyCollection<TicketClientModel>> GetTicketsByUserAsync()
        {
            string token = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            IReadOnlyCollection<TicketWithMovie> ticketsWithMovie = await ticketWithMovieService.GetByTokenAsync(token);
            return ticketsWithMovie.Select(ticketWithMovieConverter.Convert).ToArray();
        }

        [HttpPost("order")]
        public string Order([FromBody] TicketOrderRequest ticketOrderRequest)
        {
            orderTicketUseCase.OrderAsync(ticketOrderRequest.TicketId, ticketOrderRequest.Token);
            return "Ordered successfully";
        }
    }
}