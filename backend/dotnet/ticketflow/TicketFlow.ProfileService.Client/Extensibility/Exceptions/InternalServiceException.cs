using System;

namespace TicketFlow.ProfileService.Client.Extensibility.Exceptions
{
    public class InternalServiceException : Exception
    {
        public InternalServiceException(string message)
            : base(message)
        {
        }
    }
}