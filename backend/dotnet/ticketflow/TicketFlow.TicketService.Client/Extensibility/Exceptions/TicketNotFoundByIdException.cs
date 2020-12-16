using TicketFlow.Common.Exceptions;

namespace TicketFlow.TicketService.Client.Extensibility.Exceptions
{
    public class TicketNotFoundByIdException : NotFoundException
    {
        public TicketNotFoundByIdException(string message)
            : base(message)
        {
        }
    }
}