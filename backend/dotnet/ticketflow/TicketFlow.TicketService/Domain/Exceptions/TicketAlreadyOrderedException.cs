using System;

namespace TicketFlow.TicketService.Domain.Exceptions
{
    internal class TicketAlreadyOrderedException : Exception
    {
        public TicketAlreadyOrderedException(string message)
            : base(message)
        {
        }
    }
}