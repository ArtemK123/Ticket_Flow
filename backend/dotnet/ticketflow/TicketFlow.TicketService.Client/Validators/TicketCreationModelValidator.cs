using System.Collections.Generic;
using TicketFlow.TicketService.Client.Extensibility.Models;

namespace TicketFlow.TicketService.Client.Validators
{
    internal class TicketCreationModelValidator : ITicketCreationModelValidator
    {
        private const string ShouldBePossibleExceptionMessage = "{0} should be positive";

        public IReadOnlyCollection<string> Validate(TicketCreationModel ticketCreationModel)
        {
            List<string> validationMessages = new List<string>();

            if (ticketCreationModel.Row < 0)
            {
                validationMessages.Add(string.Format(ShouldBePossibleExceptionMessage, "Row number"));
            }

            if (ticketCreationModel.Seat < 0)
            {
                validationMessages.Add(string.Format(ShouldBePossibleExceptionMessage, "Seat number"));
            }

            if (ticketCreationModel.Price < 0)
            {
                validationMessages.Add(string.Format(ShouldBePossibleExceptionMessage, "Price"));
            }

            return validationMessages;
        }
    }
}