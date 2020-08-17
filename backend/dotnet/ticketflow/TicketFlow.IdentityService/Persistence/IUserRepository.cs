using TicketFlow.Common.Repositories;
using TicketFlow.IdentityService.Client.Extensibility.Entities;

namespace TicketFlow.IdentityService.Persistence
{
    internal interface IUserRepository : ICrudRepository<string, IUser>
    {
        bool TryGetByToken(string token, out IAuthorizedUser authorizedUser);
    }
}