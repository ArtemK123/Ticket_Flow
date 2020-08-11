using System.Collections.Generic;
using TicketFlow.TicketService.Domain.Models;

namespace TicketFlow.TicketService.Service.Validators
{
    internal interface ITicketCreationModelValidator
    {
        IReadOnlyCollection<string> Validate(TicketCreationModel ticketCreationModel);
    }
}