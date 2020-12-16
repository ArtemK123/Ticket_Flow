using TicketFlow.Common.Exceptions;

namespace TicketFlow.IdentityService.Client.Extensibility.Exceptions
{
    public class UserNotFoundByTokenException : NotFoundException
    {
        public UserNotFoundByTokenException(string message)
            : base(message)
        {
        }
    }
}