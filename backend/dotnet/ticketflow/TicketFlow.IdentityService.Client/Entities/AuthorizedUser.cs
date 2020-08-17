using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Generators;

namespace TicketFlow.IdentityService.Client.Entities
{
    internal class AuthorizedUser : User, IAuthorizedUser
    {
        public AuthorizedUser(string email, Role role, string storedPassword, IJwtGenerator jwtGenerator, string token)
            : base(email, role, storedPassword, jwtGenerator)
        {
            Token = token;
        }

        public string Token { get; }

        public override bool TryAuthorize(string password, out IAuthorizedUser authorizedUser)
        {
            authorizedUser = this;
            return true;
        }
    }
}