using TicketFlow.Common.Exceptions;

namespace TicketFlow.IdentityService.Client.Extensibility.Exceptions
{
    public class UserNotFoundByEmailException : NotFoundException
    {
        public UserNotFoundByEmailException(string message)
            : base(message)
        {
        }
    }
}