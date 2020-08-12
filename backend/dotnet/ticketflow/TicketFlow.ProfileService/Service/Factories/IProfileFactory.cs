using TicketFlow.Common.Factories;
using TicketFlow.ProfileService.Domain.Entities;
using TicketFlow.ProfileService.Domain.Models;

namespace TicketFlow.ProfileService.Service.Factories
{
    public interface IProfileFactory : IEntityFactory<IProfile, ProfileCreationModel>, IEntityFactory<IProfile, StoredProfileCreationModel>
    {
    }
}