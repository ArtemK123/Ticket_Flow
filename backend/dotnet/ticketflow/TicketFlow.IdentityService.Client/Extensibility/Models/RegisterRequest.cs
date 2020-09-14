namespace TicketFlow.IdentityService.Client.Extensibility.Models
{
    public class RegisterRequest
    {
        public RegisterRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}