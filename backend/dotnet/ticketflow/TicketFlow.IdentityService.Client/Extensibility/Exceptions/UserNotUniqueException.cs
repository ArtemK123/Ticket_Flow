using TicketFlow.Common.Exceptions;

namespace TicketFlow.IdentityService.Client.Extensibility.Exceptions
{
    public class UserNotUniqueException : NotUniqueEntityException
    {
        public UserNotUniqueException(string message)
            : base(message)
        {
        }
    }
}