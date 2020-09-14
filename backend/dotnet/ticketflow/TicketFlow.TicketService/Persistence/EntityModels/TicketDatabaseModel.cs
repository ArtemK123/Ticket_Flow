namespace TicketFlow.TicketService.Persistence.EntityModels
{
    public class TicketDatabaseModel
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string BuyerEmail { get; set; }

        public int Row { get; set; }

        public int Seat { get; set; }

        public int Price { get; set; }
    }
}