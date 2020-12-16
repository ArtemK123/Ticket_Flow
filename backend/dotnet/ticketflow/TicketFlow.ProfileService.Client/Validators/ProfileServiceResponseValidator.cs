using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.WebApi.Handlers;
using TicketFlow.ProfileService.Client.Extensibility.Exceptions;

namespace TicketFlow.ProfileService.Client.Validators
{
    internal class ProfileServiceResponseValidator : IProfileServiceResponseValidator
    {
        private readonly IExceptionHeaderHandler exceptionHeaderHandler;

        public ProfileServiceResponseValidator(IExceptionHeaderHandler exceptionHeaderHandler)
        {
            this.exceptionHeaderHandler = exceptionHeaderHandler;
        }

        public async Task ValidateAsync(HttpResponseMessage httpResponse)
        {
            // if (httpResponse.StatusCode == HttpStatusCode.NotFound && exceptionHeaderHandler.IsExceptionInHeader<ProfileNotFoundByIdException>(httpResponse.Headers))
            //     case HttpStatusCode.NotFound:
            //         throw new NotFoundException(await httpResponse.Content.ReadAsStringAsync());
            //     case HttpStatusCode.BadRequest: throw new NotUniqueEntityException(await httpResponse.Content.ReadAsStringAsync());
            //     case HttpStatusCode.InternalServerError: throw new InternalServiceException("Internal error in Profile Service");
            // }



            // switch (httpResponse.StatusCode)
            // {
            //     case HttpStatusCode.NotFound: throw new NotFoundException(await httpResponse.Content.ReadAsStringAsync());
            //     case HttpStatusCode.BadRequest: throw new NotUniqueEntityException(await httpResponse.Content.ReadAsStringAsync());
            //     case HttpStatusCode.InternalServerError: throw new InternalServiceException("Internal error in Profile Service");
            // }
        }
    }
}