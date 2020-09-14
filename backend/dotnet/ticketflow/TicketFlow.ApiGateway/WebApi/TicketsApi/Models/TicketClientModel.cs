using TicketFlow.ApiGateway.WebApi.MoviesApi.Models;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Models;

namespace TicketFlow.ApiGateway.WebApi.TicketsApi.Models
{
    public class TicketClientModel
    {
        public TicketClientModel()
        {
        }

        public TicketClientModel(TicketSerializationModel ticketSerializationModel, IMovie movie)
        {
            Id = ticketSerializationModel.Id;
            BuyerEmail = ticketSerializationModel.BuyerEmail;
            Row = ticketSerializationModel.Row;
            Seat = ticketSerializationModel.Seat;
            Price = ticketSerializationModel.Price;
            Movie = new ShortMovieModel(movie);
        }

        public int Id { get; set; }

        public string BuyerEmail { get; set; }

        public int Row { get; set; }

        public int Seat { get; set; }

        public int Price { get; set; }

        public ShortMovieModel Movie { get; set; }
    }
}