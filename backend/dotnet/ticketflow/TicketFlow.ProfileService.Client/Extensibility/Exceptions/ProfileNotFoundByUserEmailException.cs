using TicketFlow.Common.Exceptions;

namespace TicketFlow.ProfileService.Client.Extensibility.Exceptions
{
    public class ProfileNotFoundByUserEmailException : NotFoundException
    {
        public ProfileNotFoundByUserEmailException(string message)
            : base(message)
        {
        }
    }
}