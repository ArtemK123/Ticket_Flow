using System;

namespace TicketFlow.IdentityService.Domain.Exceptions
{
    internal class NotUniqueEntityException : Exception
    {
        public NotUniqueEntityException(string message)
            : base(message)
        {
        }
    }
}