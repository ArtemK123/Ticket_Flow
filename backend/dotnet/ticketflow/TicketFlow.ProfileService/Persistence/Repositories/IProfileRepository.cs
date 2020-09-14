using TicketFlow.Common.Repositories;
using TicketFlow.ProfileService.Client.Extensibility.Entities;

namespace TicketFlow.ProfileService.Persistence.Repositories
{
    internal interface IProfileRepository : ICrudRepository<int, IProfile>
    {
        bool TryGetByUserEmail(string email, out IProfile profile);
    }
}