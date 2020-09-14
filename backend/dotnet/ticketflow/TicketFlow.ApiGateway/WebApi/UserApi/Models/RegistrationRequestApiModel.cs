namespace TicketFlow.ApiGateway.WebApi.UserApi.Models
{
    public class RegistrationRequestApiModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ProfileClientModel Profile { get; set; }
    }
}