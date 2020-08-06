using System;

namespace TicketFlow.IdentityService.Entities.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}