using System;

namespace TicketFlow.IdentityService.Service.Entities.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}