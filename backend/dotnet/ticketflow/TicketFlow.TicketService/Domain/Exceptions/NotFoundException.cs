using System;

namespace TicketFlow.TicketService.Domain.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}