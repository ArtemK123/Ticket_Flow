using System;

namespace TicketFlow.IdentityService.Service.Entities.Exceptions
{
    internal class WrongPasswordException : Exception
    {
        public WrongPasswordException(string message)
            : base(message)
        {
        }
    }
}