namespace TicketFlow.IdentityService.Service.Entities
{
    public class User
    {
        public User(string email, string password, Role role, string optionalToken)
        {
            Email = email;
            Password = password;
            Role = role;
            Token = optionalToken;
        }

        public User(string email, string password, int roleNumber, string optionalToken)
        {
            Email = email;
            Password = password;
            Role = (Role)roleNumber;
            Token = optionalToken;
        }

        public string Email { get; }

        public string Password { get; }

        public Role Role { get; }

        public string Token { get; set; }
    }
}