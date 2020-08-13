using TicketFlow.Common.Factories;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Models;

namespace TicketFlow.ProfileService.Client.Extensibility.Factories
{
    public interface IProfileFactory : IEntityFactory<IProfile, ProfileCreationModel>, IEntityFactory<IProfile, StoredProfileCreationModel>
    {
    }
}