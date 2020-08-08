namespace TicketFlow.TicketService.WebApi.ClientModels.Requests
{
    public class OrderRequestModel
    {
        public int TicketId { get; set; }

        public string BuyerEmail { get; set; }
    }
}