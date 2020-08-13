using System;

namespace TicketFlow.ProfileService.Client.Extensibility.Exceptions
{
    public class NotUniqueEntityException : Exception
    {
        public NotUniqueEntityException(string message)
            : base(message)
        {
        }
    }
}