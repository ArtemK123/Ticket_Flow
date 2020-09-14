using System;

namespace TicketFlow.TicketService.Client.Extensibility.Exceptions
{
    public class TicketAlreadyOrderedException : Exception
    {
        public TicketAlreadyOrderedException(string message)
            : base(message)
        {
        }
    }
}