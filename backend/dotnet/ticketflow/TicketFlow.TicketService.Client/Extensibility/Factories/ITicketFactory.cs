using TicketFlow.Common.Factories;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Models;

namespace TicketFlow.TicketService.Client.Extensibility.Factories
{
    public interface ITicketFactory :
            IEntityFactory<ITicket, TicketCreationModel>,
            IEntityFactory<ITicket, StoredTicketCreationModel>,
            IEntityFactory<IOrderedTicket, OrderedTicketCreationModel>
    {
    }
}