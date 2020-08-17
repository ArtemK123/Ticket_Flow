using TicketFlow.IdentityService.Client.Entities;

namespace TicketFlow.IdentityService.Client.Extensibility.Models
{
    public class UserCreationModel
    {
        public UserCreationModel(string email, string password, Role role)
        {
            Email = email;
            Password = password;
            Role = role;
        }

        public string Email { get; }

        public string Password { get; }

        public Role Role { get; }
    }
}