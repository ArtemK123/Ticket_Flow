namespace TicketFlow.TicketService.Entities
{
    public interface ITicket
    {
        public int Id { get; }

        public int MovieId { get; }

        public string BuyerEmail { get; }

        public int Row { get; }

        public int Seat { get; }

        public int Price { get; }
    }
}