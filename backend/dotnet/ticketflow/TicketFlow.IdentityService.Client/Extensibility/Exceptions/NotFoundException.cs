using System;

namespace TicketFlow.IdentityService.Client.Extensibility.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}