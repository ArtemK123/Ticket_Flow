using TicketFlow.Common.Factories;
using TicketFlow.TicketService.Domain.Entities;
using TicketFlow.TicketService.Domain.Models;

namespace TicketFlow.TicketService.Service.Factories
{
    public interface ITicketFactory :
            IEntityFactory<ITicket, TicketCreationModel>,
            IEntityFactory<ITicket, StoredTicketCreationModel>,
            IEntityFactory<IOrderedTicket, OrderedTicketCreationModel>
    {
    }
}