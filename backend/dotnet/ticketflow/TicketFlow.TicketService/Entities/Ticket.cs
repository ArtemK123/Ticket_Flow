namespace TicketFlow.TicketService.Entities
{
    internal class Ticket : ITicket
    {
        public Ticket(int id, int movieId, string buyerEmail, int row, int seat, int price)
        {
            Id = id;
            MovieId = movieId;
            BuyerEmail = buyerEmail;
            Row = row;
            Seat = seat;
            Price = price;
        }

        public int Id { get; }

        public int MovieId { get; }

        public string BuyerEmail { get; }

        public int Row { get; }

        public int Seat { get; }

        public int Price { get; }
    }
}