using TicketFlow.Common.Exceptions;

namespace TicketFlow.IdentityService.Client.Extensibility.Exceptions
{
    public class UserNotFoundByIdException : NotFoundException
    {
        public UserNotFoundByIdException(string message)
            : base(message)
        {
        }
    }
}