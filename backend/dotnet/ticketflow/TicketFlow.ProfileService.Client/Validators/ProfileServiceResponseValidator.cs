using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.Common.Exceptions;

namespace TicketFlow.ProfileService.Client.Validators
{
    internal class ProfileServiceResponseValidator : IProfileServiceResponseValidator
    {
        public async Task ValidateAsync(HttpResponseMessage httpResponse)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.NotFound: throw new NotFoundException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: throw new NotUniqueEntityException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: throw new InternalServiceException("Internal error in Profile Service");
            }
        }
    }
}