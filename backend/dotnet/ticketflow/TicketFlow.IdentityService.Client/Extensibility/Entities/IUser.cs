using TicketFlow.IdentityService.Client.Entities;

namespace TicketFlow.IdentityService.Client.Extensibility.Entities
{
    public interface IUser
    {
        public string Email { get; }

        public string Password { get; }

        public Role Role { get; }

        public bool TryAuthorize(string password, out IAuthorizedUser authorizedUser);
    }
}