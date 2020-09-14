using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Generators;

namespace TicketFlow.IdentityService.Client.Entities
{
    internal class User : IUser
    {
        private readonly IJwtGenerator jwtGenerator;

        public User(string email, Role role, string password, IJwtGenerator jwtGenerator)
        {
            Email = email;
            Role = role;
            Password = password;
            this.jwtGenerator = jwtGenerator;
        }

        public string Email { get; }

        public string Password { get; }

        public Role Role { get; }

        public virtual bool TryAuthorize(string password, out IAuthorizedUser authorizedUser)
        {
            authorizedUser = default;
            if (!Password.Equals(password))
            {
                return false;
            }

            string token = jwtGenerator.Generate(this);
            authorizedUser = new AuthorizedUser(Email, Role, Password, jwtGenerator, token);
            return true;
        }
    }
}