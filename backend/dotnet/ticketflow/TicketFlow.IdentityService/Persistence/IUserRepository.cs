using TicketFlow.IdentityService.Entities;

namespace TicketFlow.IdentityService.Persistence
{
    internal interface IUserRepository
    {
        bool TryGetByToken(string token, out User user);

        bool TryGetByEmail(string email, out User user);

        void Update(User user);

        void Add(User user);
    }
}