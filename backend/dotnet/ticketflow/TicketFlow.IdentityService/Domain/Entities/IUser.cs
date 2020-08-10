namespace TicketFlow.IdentityService.Domain.Entities
{
    public interface IUser
    {
        public string Email { get; }

        public Role Role { get; }

        public bool TryAuthorize(string password, out IAuthorizedUser authorizedUser);
    }
}