using System.Collections.Generic;
using System.Threading.Tasks;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Proxies;

namespace TicketFlow.ApiGateway.Service
{
    internal class AddMovieUseCase : IAddMovieUseCase
    {
        private const int DefaultTicketPrice = 50;

        private readonly IMovieApiProxy movieApiProxy;
        private readonly ITicketApiProxy ticketApiProxy;

        public AddMovieUseCase(IMovieApiProxy movieApiProxy, ITicketApiProxy ticketApiProxy)
        {
            this.movieApiProxy = movieApiProxy;
            this.ticketApiProxy = ticketApiProxy;
        }

        public async Task AddAsync(IMovie movie)
        {
            int createdMovieId = await movieApiProxy.AddAsync(new MovieCreationIdReferencedModel(movie.StartTime, movie.Film.Id, movie.CinemaHall.Id));

            var tasks = new List<Task>();
            for (int row = 1; row <= movie.CinemaHall.SeatRows; row++)
            {
                for (int seat = 1; seat <= movie.CinemaHall.SeatsInRow; seat++)
                {
                    tasks.Add(ticketApiProxy.AddAsync(new TicketCreationModel(createdMovieId, row, seat, DefaultTicketPrice)));
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}