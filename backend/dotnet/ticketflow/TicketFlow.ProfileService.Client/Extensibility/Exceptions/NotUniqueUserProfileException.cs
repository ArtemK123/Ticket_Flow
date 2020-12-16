using TicketFlow.Common.Exceptions;

namespace TicketFlow.ProfileService.Client.Extensibility.Exceptions
{
    public class NotUniqueUserProfileException : NotUniqueEntityException
    {
        public NotUniqueUserProfileException(string message)
            : base(message)
        {
        }
    }
}