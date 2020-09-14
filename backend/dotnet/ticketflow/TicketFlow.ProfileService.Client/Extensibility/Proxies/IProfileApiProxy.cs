using System.Threading.Tasks;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Models;

namespace TicketFlow.ProfileService.Client.Extensibility.Proxies
{
    public interface IProfileApiProxy
    {
        Task<IProfile> GetByIdAsync(int id);

        Task<IProfile> GetByUserEmailAsync(string email);

        Task<IProfile> AddAsync(ProfileCreationModel profileCreationModel);
    }
}