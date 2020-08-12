namespace TicketFlow.TicketService.Client.Extensibility.Models
{
    public class StoredTicketCreationModel : TicketCreationModel
    {
        public StoredTicketCreationModel(int id, int movieId, int row, int seat, int price)
            : base(movieId, row, seat, price)
        {
            Id = id;
        }

        public int Id { get; }
    }
}