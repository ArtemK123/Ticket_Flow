using TicketFlow.Common.Serializers;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Models;

namespace TicketFlow.ProfileService.Client.Extensibility.Serializers
{
    public interface IProfileSerializer : IEntitySerializer<IProfile, ProfileSerializationModel>
    {
    }
}