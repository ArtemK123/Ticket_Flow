using System;

namespace TicketFlow.Common.Exceptions
{
    public class NotUniqueEntityException : Exception
    {
        public NotUniqueEntityException(string message)
            : base(message)
        {
        }
    }
}