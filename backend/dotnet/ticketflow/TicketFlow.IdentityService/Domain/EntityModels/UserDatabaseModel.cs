namespace TicketFlow.IdentityService.Domain.EntityModels
{
    public class UserDatabaseModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }

        public string Token { get; set; }
    }
}