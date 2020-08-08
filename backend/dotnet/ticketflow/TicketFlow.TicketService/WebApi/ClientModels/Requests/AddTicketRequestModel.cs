namespace TicketFlow.TicketService.WebApi.ClientModels.Requests
{
    public class AddTicketRequestModel
    {
        public int MovieId { get; set; }

        public string BuyerEmail { get; set; }

        public int Row { get; set; }

        public int Seat { get; set; }

        public int Price { get; set; }
    }
}