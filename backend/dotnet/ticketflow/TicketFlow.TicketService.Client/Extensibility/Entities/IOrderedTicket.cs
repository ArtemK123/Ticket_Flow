namespace TicketFlow.TicketService.Client.Extensibility.Entities
{
    public interface IOrderedTicket : ITicket
    {
        public string BuyerEmail { get; }
    }
}