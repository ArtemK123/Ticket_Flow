namespace TicketFlow.IdentityService.Api.ClientModels.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}