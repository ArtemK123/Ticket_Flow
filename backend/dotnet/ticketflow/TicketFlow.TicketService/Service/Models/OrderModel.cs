namespace TicketFlow.TicketService.Service.Models
{
    public class OrderModel
    {
        public OrderModel(int ticketId, string buyerEmail)
        {
            TicketId = ticketId;
            BuyerEmail = buyerEmail;
        }

        public int TicketId { get; }

        public string BuyerEmail { get; }
    }
}