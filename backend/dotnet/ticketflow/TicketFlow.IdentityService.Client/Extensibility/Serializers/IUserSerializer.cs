using TicketFlow.Common.Serializers;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;

namespace TicketFlow.IdentityService.Client.Extensibility.Serializers
{
    public interface IUserSerializer : IEntitySerializer<IUser, UserSerializationModel>
    {
    }
}