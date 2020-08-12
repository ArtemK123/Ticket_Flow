using System;

namespace TicketFlow.TicketService.Client.Extensibility.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}