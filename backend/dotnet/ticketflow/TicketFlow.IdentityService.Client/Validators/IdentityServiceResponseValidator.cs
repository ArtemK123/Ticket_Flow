using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.WebApi.Handlers;
using TicketFlow.IdentityService.Client.Extensibility.Exceptions;

namespace TicketFlow.IdentityService.Client.Validators
{
    internal class IdentityServiceResponseValidator : IIdentityServiceResponseValidator
    {
        private readonly IExceptionHeaderHandler exceptionHeaderHandler;

        public IdentityServiceResponseValidator(IExceptionHeaderHandler exceptionHeaderHandler)
        {
            this.exceptionHeaderHandler = exceptionHeaderHandler;
        }

        public async Task ValidateAsync(HttpResponseMessage httpResponse)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new UserNotUniqueException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized:
                    throw new WrongPasswordException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.NotFound when exceptionHeaderHandler.IsExceptionInHeader<UserNotFoundByTokenException>(httpResponse.Headers):
                    throw new UserNotFoundByTokenException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.NotFound when exceptionHeaderHandler.IsExceptionInHeader<UserNotFoundByEmailException>(httpResponse.Headers):
                    throw new UserNotFoundByEmailException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError:
                    throw new InternalServiceException("Internal error in Identity Service");
            }
        }
    }
}