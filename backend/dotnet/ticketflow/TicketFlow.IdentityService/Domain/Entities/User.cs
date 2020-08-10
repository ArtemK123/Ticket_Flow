using TicketFlow.IdentityService.Service;

namespace TicketFlow.IdentityService.Domain.Entities
{
    internal class User : IUser
    {
        private readonly IJwtGenerator jwtGenerator;
        private readonly string storedPassword;

        public User(string email, Role role, string password, IJwtGenerator jwtGenerator)
        {
            Email = email;
            Role = role;
            storedPassword = password;
            this.jwtGenerator = jwtGenerator;
        }

        public string Email { get; }

        public Role Role { get; }

        public virtual bool TryAuthorize(string password, out IAuthorizedUser authorizedUser)
        {
            authorizedUser = default;
            if (!storedPassword.Equals(password))
            {
                return false;
            }

            string token = jwtGenerator.Generate(this);
            authorizedUser = new AuthorizedUser(Email, Role, storedPassword, jwtGenerator, token);
            return true;
        }
    }
}