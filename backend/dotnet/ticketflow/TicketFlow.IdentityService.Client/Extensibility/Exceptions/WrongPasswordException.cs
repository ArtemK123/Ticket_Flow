using System;

namespace TicketFlow.IdentityService.Client.Extensibility.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException(string message)
            : base(message)
        {
        }
    }
}