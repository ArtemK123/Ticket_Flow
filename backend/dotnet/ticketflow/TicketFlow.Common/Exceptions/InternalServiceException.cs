using System;

namespace TicketFlow.Common.Exceptions
{
    public class InternalServiceException : Exception
    {
        public InternalServiceException(string message)
            : base(message)
        {
        }
    }
}