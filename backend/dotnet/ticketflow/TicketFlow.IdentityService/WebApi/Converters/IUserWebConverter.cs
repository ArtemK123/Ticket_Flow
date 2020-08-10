using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.WebApi.ClientModels.Responses;

namespace TicketFlow.IdentityService.WebApi.Converters
{
    public interface IUserWebConverter
    {
        UserWebModel Convert(IUser user);

        UserWebModel Convert(IAuthorizedUser authorizedUser);
    }
}