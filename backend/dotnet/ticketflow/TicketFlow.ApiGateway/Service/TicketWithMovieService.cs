using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketFlow.ApiGateway.Models;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Proxies;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Proxies;

namespace TicketFlow.ApiGateway.Service
{
    internal class TicketWithMovieService : ITicketWithMovieService
    {
        private readonly ITicketApiProxy ticketApiProxy;
        private readonly IMovieApiProxy movieApiProxy;
        private readonly IUserApiProxy userApiProxy;

        public TicketWithMovieService(ITicketApiProxy ticketApiProxy, IMovieApiProxy movieApiProxy, IUserApiProxy userApiProxy)
        {
            this.ticketApiProxy = ticketApiProxy;
            this.movieApiProxy = movieApiProxy;
            this.userApiProxy = userApiProxy;
        }

        public async Task<IReadOnlyCollection<TicketWithMovie>> GetByMovieIdAsync(int movieId)
        {
            IMovie movie = await movieApiProxy.GetByIdAsync(movieId);
            IReadOnlyCollection<ITicket> tickets = await ticketApiProxy.GetByMovieIdAsync(movieId);

            return tickets.Select(ticket => new TicketWithMovie(ticket, movie)).ToArray();
        }

        public async Task<IReadOnlyCollection<TicketWithMovie>> GetByTokenAsync(string token)
        {
            IAuthorizedUser user = await userApiProxy.GetByTokenAsync(token);
            IReadOnlyCollection<ITicket> tickets = await ticketApiProxy.GetByUserEmailAsync(user.Email);

            if (!tickets.Any())
            {
                return Array.Empty<TicketWithMovie>();
            }

            IMovie movie = await movieApiProxy.GetByIdAsync(tickets.First().MovieId);
            return tickets.Select(ticket => new TicketWithMovie(ticket, movie)).ToArray();
        }
    }
}