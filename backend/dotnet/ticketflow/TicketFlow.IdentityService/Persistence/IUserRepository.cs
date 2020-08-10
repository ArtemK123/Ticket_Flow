using TicketFlow.IdentityService.Domain.Entities;

namespace TicketFlow.IdentityService.Persistence
{
    internal interface IUserRepository
    {
        bool TryGetByToken(string token, out IAuthorizedUser authorizedUser);

        bool TryGetByEmail(string email, out IUser user);

        void Update(IUser user);

        void Add(IUser user);
    }
}