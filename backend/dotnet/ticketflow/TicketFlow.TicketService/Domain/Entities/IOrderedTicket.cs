namespace TicketFlow.TicketService.Domain.Entities
{
    public interface IOrderedTicket : ITicket
    {
        public string BuyerEmail { get; }
    }
}