using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.MovieService.Client.Extensibility.Exceptions;

namespace TicketFlow.MovieService.Client.Validators
{
    internal class MovieServiceResponseValidator : IMovieServiceResponseValidator
    {
        public async Task ValidateAsync(HttpResponseMessage httpResponse)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.NotFound: throw new NotFoundException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: throw new InternalServiceException("Internal error in Movie Service");
            }
        }
    }
}