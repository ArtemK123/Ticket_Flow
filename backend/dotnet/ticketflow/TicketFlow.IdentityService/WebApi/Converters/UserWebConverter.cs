using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.WebApi.ClientModels.Responses;

namespace TicketFlow.IdentityService.WebApi.Converters
{
    internal class UserWebConverter : IUserWebConverter
    {
        public UserWebModel Convert(IUser user) => new UserWebModel(user.Email, (int)user.Role, null);

        public UserWebModel Convert(IAuthorizedUser authorizedUser) => new UserWebModel(authorizedUser.Email, (int)authorizedUser.Role, authorizedUser.Token);
    }
}