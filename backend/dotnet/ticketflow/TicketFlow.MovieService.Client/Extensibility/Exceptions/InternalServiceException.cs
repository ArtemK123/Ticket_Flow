using System;

namespace TicketFlow.MovieService.Client.Extensibility.Exceptions
{
    public class InternalServiceException : Exception
    {
        public InternalServiceException(string message)
            : base(message)
        {
        }
    }
}