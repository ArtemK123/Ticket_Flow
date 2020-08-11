using TicketFlow.Common.Factories;
using TicketFlow.ProfileService.Domain.Entities;
using TicketFlow.ProfileService.Domain.Models;

namespace TicketFlow.ProfileService.Service.Serializers
{
    public interface IProfileSerializer : IEntityFactory<IProfile, ProfileSerializationModel>
    {
        ProfileSerializationModel Serialize(IProfile profile);
    }
}