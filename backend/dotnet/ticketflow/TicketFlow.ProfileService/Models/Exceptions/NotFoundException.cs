using System;

namespace TicketFlow.ProfileService.Models.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}