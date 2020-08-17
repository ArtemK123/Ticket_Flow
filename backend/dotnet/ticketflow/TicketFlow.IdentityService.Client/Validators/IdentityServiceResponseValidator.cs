using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.IdentityService.Client.Extensibility.Exceptions;

namespace TicketFlow.IdentityService.Client.Validators
{
    internal class IdentityServiceResponseValidator : IIdentityServiceResponseValidator
    {
        public async Task ValidateAsync(HttpResponseMessage httpResponse)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.BadRequest: throw new NotUniqueEntityException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: throw new WrongPasswordException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.NotFound: throw new NotFoundException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: throw new InternalServiceException("Internal error in Identity Service");
            }
        }
    }
}