namespace TicketFlow.TicketService.Client.Extensibility.Models
{
    public class TicketCreationModel
    {
        public TicketCreationModel(int movieId, int row, int seat, int price)
        {
            MovieId = movieId;
            Row = row;
            Seat = seat;
            Price = price;
        }

        public int MovieId { get; }

        public int Row { get; }

        public int Seat { get; }

        public int Price { get; }
    }
}