namespace TicketFlow.TicketService.Service.Models
{
    public class TicketModelWithoutId
    {
        public TicketModelWithoutId(int movieId, string buyerEmail, int row, int seat, int price)
        {
            MovieId = movieId;
            BuyerEmail = buyerEmail;
            Row = row;
            Seat = seat;
            Price = price;
        }

        public int MovieId { get; }

        public string BuyerEmail { get; }

        public int Row { get; }

        public int Seat { get; }

        public int Price { get; }
    }
}