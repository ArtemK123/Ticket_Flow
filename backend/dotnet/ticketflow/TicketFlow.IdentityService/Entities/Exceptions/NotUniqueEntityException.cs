using System;

namespace TicketFlow.IdentityService.Entities.Exceptions
{
    internal class NotUniqueEntityException : Exception
    {
        public NotUniqueEntityException(string message)
            : base(message)
        {
        }
    }
}