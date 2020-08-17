using System;

namespace TicketFlow.MovieService.Client.Extensibility.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}