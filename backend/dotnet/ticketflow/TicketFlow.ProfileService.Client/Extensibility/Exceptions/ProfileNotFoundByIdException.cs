using TicketFlow.Common.Exceptions;

namespace TicketFlow.ProfileService.Client.Extensibility.Exceptions
{
    public class ProfileNotFoundByIdException : NotFoundException
    {
        public ProfileNotFoundByIdException(string message)
            : base(message)
        {
        }
    }
}