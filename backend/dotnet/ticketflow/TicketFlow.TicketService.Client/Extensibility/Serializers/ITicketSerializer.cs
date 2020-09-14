using TicketFlow.Common.Serializers;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Models;

namespace TicketFlow.TicketService.Client.Extensibility.Serializers
{
    public interface ITicketSerializer : IEntitySerializer<ITicket, TicketSerializationModel>
    {
    }
}