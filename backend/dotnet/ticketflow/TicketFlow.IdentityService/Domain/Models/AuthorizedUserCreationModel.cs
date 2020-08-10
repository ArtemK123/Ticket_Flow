using TicketFlow.IdentityService.Domain.Entities;

namespace TicketFlow.IdentityService.Domain.Models
{
    public class AuthorizedUserCreationModel : UserCreationModel
    {
        public AuthorizedUserCreationModel(string email, string password, Role role, string token)
            : base(email, password, role)
        {
            Token = token;
        }

        public string Token { get; }
    }
}