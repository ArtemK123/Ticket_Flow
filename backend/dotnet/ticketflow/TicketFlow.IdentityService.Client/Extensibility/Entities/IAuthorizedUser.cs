namespace TicketFlow.IdentityService.Client.Extensibility.Entities
{
    public interface IAuthorizedUser : IUser
    {
        string Token { get; }
    }
}