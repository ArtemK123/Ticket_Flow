namespace TicketFlow.IdentityService.Domain.Models
{
    public class UserSerializationModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }

        public string Token { get; set; }
    }
}