using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.TicketService.Client.Extensibility.Exceptions;

namespace TicketFlow.TicketService.Client.Validators
{
    internal class TicketServiceResponseValidator : ITicketServiceResponseValidator
    {
        public async Task ValidateAsync(HttpResponseMessage httpResponse)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.NotFound: throw new NotFoundException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: throw new TicketAlreadyOrderedException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: throw new InternalServiceException();
            }
        }
    }
}