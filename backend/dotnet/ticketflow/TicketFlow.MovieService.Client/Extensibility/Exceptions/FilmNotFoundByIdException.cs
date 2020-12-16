using TicketFlow.Common.Exceptions;

namespace TicketFlow.MovieService.Client.Extensibility.Exceptions
{
    public class FilmNotFoundByIdException : NotFoundException
    {
        public FilmNotFoundByIdException(string message)
            : base(message)
        {
        }
    }
}