namespace TicketFlow.TicketService.Client.Extensibility.Models
{
    public class OrderedTicketCreationModel : StoredTicketCreationModel
    {
        public OrderedTicketCreationModel(int id, int movieId, int row, int seat, int price, string buyerEmail)
            : base(id, movieId, row, seat, price)
        {
            BuyerEmail = buyerEmail;
        }

        public string BuyerEmail { get; }
    }
}