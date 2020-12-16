using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.WebApi.Handlers;
using TicketFlow.MovieService.Client.Extensibility.Exceptions;

namespace TicketFlow.MovieService.Client.Validators
{
    internal class MovieServiceResponseValidator : IMovieServiceResponseValidator
    {
        private readonly IExceptionHeaderHandler exceptionHeaderHandler;

        public MovieServiceResponseValidator(IExceptionHeaderHandler exceptionHeaderHandler)
        {
            this.exceptionHeaderHandler = exceptionHeaderHandler;
        }

        public async Task ValidateAsync(HttpResponseMessage httpResponse)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.NotFound when exceptionHeaderHandler.IsExceptionInHeader<CinemaHallNotFoundByIdException>(httpResponse.Headers):
                    throw new CinemaHallNotFoundByIdException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.NotFound when exceptionHeaderHandler.IsExceptionInHeader<FilmNotFoundByIdException>(httpResponse.Headers):
                    throw new FilmNotFoundByIdException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.NotFound when exceptionHeaderHandler.IsExceptionInHeader<MovieNotFoundByIdException>(httpResponse.Headers):
                    throw new MovieNotFoundByIdException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: throw new InternalServiceException("Internal error in Movie Service");
            }
        }
    }
}