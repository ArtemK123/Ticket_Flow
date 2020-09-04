using System.Threading.Tasks;
using TicketFlow.ApiGateway.Models;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Proxies;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Models;
using TicketFlow.ProfileService.Client.Extensibility.Proxies;

namespace TicketFlow.ApiGateway.Service
{
    internal class UserWithProfileService : IUserWithProfileService
    {
        private readonly IUserApiProxy userApiProxy;
        private readonly IProfileApiProxy profileApiProxy;

        public UserWithProfileService(IUserApiProxy userApiProxy, IProfileApiProxy profileApiProxy)
        {
            this.userApiProxy = userApiProxy;
            this.profileApiProxy = profileApiProxy;
        }

        public async Task RegisterAsync(UserWithProfile userWithProfile)
        {
            await userApiProxy.RegisterAsync(new RegisterRequest(userWithProfile.User.Email, userWithProfile.User.Password));
            await profileApiProxy.AddAsync(new ProfileCreationModel(userWithProfile.Profile.UserEmail, userWithProfile.Profile.PhoneNumber, userWithProfile.Profile.Birthday));
        }

        public async Task<UserWithProfile> GetUserWithProfileAsync(string token)
        {
            IAuthorizedUser user = await userApiProxy.GetByTokenAsync(token);
            IProfile profile = await profileApiProxy.GetByUserEmailAsync(user.Email);

            return new UserWithProfile(user, profile);
        }
    }
}