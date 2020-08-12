using TicketFlow.Common.Serializers;
using TicketFlow.ProfileService.Domain.Entities;
using TicketFlow.ProfileService.Domain.Models;

namespace TicketFlow.ProfileService.Service.Serializers
{
    public interface IProfileSerializer : IEntitySerializer<IProfile, ProfileSerializationModel>
    {
    }
}