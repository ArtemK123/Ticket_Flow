namespace TicketFlow.TicketService.Domain.Entities
{
    public interface ITicket
    {
        public int Id { get; }

        public int MovieId { get; }

        public int Row { get; }

        public int Seat { get; }

        public int Price { get; }

        public IOrderedTicket Order(string buyerEmail);
    }
}