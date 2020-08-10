namespace TicketFlow.IdentityService.Domain.Entities
{
    public interface IAuthorizedUser : IUser
    {
        string Token { get; }
    }
}