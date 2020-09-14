namespace TicketFlow.TicketService.Client.Extensibility.Models
{
    public class TicketSerializationModel
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int Row { get; set; }

        public int Seat { get; set; }

        public int Price { get; set; }

        public string BuyerEmail { get; set; }
    }
}