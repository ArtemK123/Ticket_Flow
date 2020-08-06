using System;

namespace TicketFlow.IdentityService.Entities.Exceptions
{
    internal class WrongPasswordException : Exception
    {
        public WrongPasswordException(string message)
            : base(message)
        {
        }
    }
}