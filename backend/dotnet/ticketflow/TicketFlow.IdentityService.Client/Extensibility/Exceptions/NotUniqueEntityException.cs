using System;

namespace TicketFlow.IdentityService.Client.Extensibility.Exceptions
{
    public class NotUniqueEntityException : Exception
    {
        public NotUniqueEntityException(string message)
            : base(message)
        {
        }
    }
}