namespace TicketFlow.TicketService.WebApi.ClientModels.Requests
{
    public class TicketCreationApiModel
    {
        public int MovieId { get; set; }

        public int Row { get; set; }

        public int Seat { get; set; }

        public int Price { get; set; }
    }
}