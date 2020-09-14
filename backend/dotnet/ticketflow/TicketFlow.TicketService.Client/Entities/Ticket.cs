using System;
using TicketFlow.TicketService.Client.Extensibility.Entities;

namespace TicketFlow.TicketService.Client.Entities
{
    internal class Ticket : ITicket
    {
        public Ticket(int id, int movieId, int row, int seat, int price)
        {
            Id = id;
            MovieId = movieId;
            Row = row;
            Seat = seat;
            Price = price;
        }

        public int Id { get; }

        public int MovieId { get; }

        public int Row { get; }

        public int Seat { get; }

        public int Price { get; }

        public IOrderedTicket Order(string buyerEmail)
        {
            if (string.IsNullOrEmpty(buyerEmail))
            {
                throw new ArgumentNullException();
            }

            return new OrderedTicket(Id, MovieId, Row, Seat, Price, buyerEmail);
        }
    }
}