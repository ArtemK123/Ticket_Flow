using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;

namespace TicketFlow.TicketService.Service.Serializers
{
    public interface ITicketSerializer
    {
        TicketSerializationModel Serialize(ITicket ticket);

        ITicket Deserialize(TicketSerializationModel serializationModel);
    }
}