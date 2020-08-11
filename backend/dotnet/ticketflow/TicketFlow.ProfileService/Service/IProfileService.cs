using TicketFlow.ProfileService.Domain.Entities;

namespace TicketFlow.ProfileService.Service
{
    public interface IProfileService
    {
        IProfile GetById(int id);

        IProfile GetByUserEmail(string email);

        void Add(IProfile profile);
    }
}