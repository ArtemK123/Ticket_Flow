using TicketFlow.Common.Exceptions;

namespace TicketFlow.MovieService.Client.Extensibility.Exceptions
{
    public class MovieNotFoundByIdException : NotFoundException
    {
        public MovieNotFoundByIdException(string message)
            : base(message)
        {
        }
    }
}