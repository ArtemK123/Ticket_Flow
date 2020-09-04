namespace TicketFlow.ApiGateway.WebApi.UserApi.Models
{
    public class GetProfileResponse
    {
        public string UserEmail { get; set; }

        public ProfileClientModel Profile { get; set; }
    }
}