using TicketFlow.IdentityService.Service;

namespace TicketFlow.IdentityService.Domain.Entities
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