using System;

namespace TicketFlow.ApiGateway.Models.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}