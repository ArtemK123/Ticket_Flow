using TicketFlow.IdentityService.Domain.Entities;

namespace TicketFlow.IdentityService.Domain.Models
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