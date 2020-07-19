using TicketFlow.ProfileService.Models;

namespace TicketFlow.ProfileService.Service
{
    public interface IProfileService
    {
        Profile GetById(int id);

        Profile GetByUserEmail(string email);

        Profile Add(Profile profile);
    }
}