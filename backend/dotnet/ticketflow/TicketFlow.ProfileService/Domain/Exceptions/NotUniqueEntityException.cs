using System;

namespace TicketFlow.ProfileService.Domain.Exceptions
{
    internal class NotUniqueEntityException : Exception
    {
        public NotUniqueEntityException(string message)
            : base(message)
        {
        }
    }
}