using System;

namespace TicketFlow.IdentityService.Client.Extensibility.Exceptions
{
    public class InternalServiceException : Exception
    {
        public InternalServiceException(string message)
            : base(message)
        {
        }
    }
}