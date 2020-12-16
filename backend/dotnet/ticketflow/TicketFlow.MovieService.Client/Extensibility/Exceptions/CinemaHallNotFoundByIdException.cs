using TicketFlow.Common.Exceptions;

namespace TicketFlow.MovieService.Client.Extensibility.Exceptions
{
    public class CinemaHallNotFoundByIdException : NotFoundException
    {
        public CinemaHallNotFoundByIdException(string message)
            : base(message)
        {
        }
    }
}