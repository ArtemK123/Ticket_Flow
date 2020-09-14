using System.Collections.Generic;
using TicketFlow.TicketService.Client.Extensibility.Models;

namespace TicketFlow.TicketService.Client.Validators
{
    internal interface ITicketCreationModelValidator
    {
        IReadOnlyCollection<string> Validate(TicketCreationModel ticketCreationModel);
    }
}