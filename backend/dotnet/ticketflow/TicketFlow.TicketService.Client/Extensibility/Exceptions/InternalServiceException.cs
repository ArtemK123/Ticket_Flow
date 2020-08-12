using System;

namespace TicketFlow.TicketService.Client.Extensibility.Exceptions
{
    public class InternalServiceException : Exception
    {
        public InternalServiceException()
            : base("Internal service error in Ticket Service")
        {
        }
    }
}