using System;

namespace TicketFlow.ProfileService.Client.Extensibility.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}