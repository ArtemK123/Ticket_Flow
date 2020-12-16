using System;

namespace TicketFlow.Common.Exceptions
{
    public class InvalidRequestModelException : Exception
    {
        public InvalidRequestModelException(string message)
            : base(message)
        {
        }
    }
}