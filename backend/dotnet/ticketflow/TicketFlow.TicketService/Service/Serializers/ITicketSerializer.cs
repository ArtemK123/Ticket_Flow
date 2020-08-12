using TicketFlow.Common.Serializers;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;

namespace TicketFlow.TicketService.Service.Serializers
{
    public interface ITicketSerializer : IEntitySerializer<ITicket, TicketSerializationModel>
    {
    }
}