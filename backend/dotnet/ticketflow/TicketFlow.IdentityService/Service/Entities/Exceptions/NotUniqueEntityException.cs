using System;

namespace TicketFlow.IdentityService.Service.Entities.Exceptions
{
    internal class NotUniqueEntityException : Exception
    {
        public NotUniqueEntityException(string message)
            : base(message)
        {
        }
    }
}