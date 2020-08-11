using TicketFlow.Common.Serializers;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Domain.Models;

namespace TicketFlow.IdentityService.Service.Serializers
{
    public interface IUserSerializer : IEntitySerializer<IUser, UserSerializationModel>
    {
    }
}