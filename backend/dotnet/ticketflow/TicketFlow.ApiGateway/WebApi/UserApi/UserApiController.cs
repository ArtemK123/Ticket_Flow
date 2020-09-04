using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.ApiGateway.Models;
using TicketFlow.ApiGateway.Service;
using TicketFlow.ApiGateway.WebApi.UserApi.Models;
using TicketFlow.Common.Readers;
using TicketFlow.IdentityService.Client.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Factories;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Proxies;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Factories;
using TicketFlow.ProfileService.Client.Extensibility.Models;

namespace TicketFlow.ApiGateway.WebApi.UserApi
{
    [ApiController]
    [Route("/")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserWithProfileService userWithProfileService;
        private readonly IUserApiProxy userApiProxy;
        private readonly IUserFactory userFactory;
        private readonly IProfileFactory profileFactory;
        private readonly IStringFromStreamReader stringFromStreamReader;

        public UserApiController(
            IUserWithProfileService userWithProfileService,
            IUserFactory userFactory,
            IProfileFactory profileFactory,
            IStringFromStreamReader stringFromStreamReader,
            IUserApiProxy userApiProxy)
        {
            this.userWithProfileService = userWithProfileService;
            this.userFactory = userFactory;
            this.profileFactory = profileFactory;
            this.stringFromStreamReader = stringFromStreamReader;
            this.userApiProxy = userApiProxy;
        }

        [HttpPost("login")]
        public async Task<string> LoginAsync([FromBody] LoginRequestApiModel loginRequestApiModel)
        {
            return await userApiProxy.LoginAsync(new LoginRequest(loginRequestApiModel.Email, loginRequestApiModel.Password));
        }

        [HttpPost("register")]
        public async Task RegisterAsync([FromBody] RegistrationRequestApiModel registrationRequest)
        {
            IUser user = userFactory.Create(new UserCreationModel(registrationRequest.Email, registrationRequest.Password, Role.User));
            IProfile profile = profileFactory.Create(new ProfileCreationModel(registrationRequest.Email, registrationRequest.Profile.PhoneNumber, registrationRequest.Profile.Birthday));

            await userWithProfileService.RegisterAsync(new UserWithProfile(user, profile));
        }

        [HttpPost("profile")]
        public async Task<GetProfileResponse> GetProfileAsync()
        {
            string token = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            UserWithProfile userWithProfile = await userWithProfileService.GetUserWithProfileAsync(token);
            return new GetProfileResponse
            {
                UserEmail = userWithProfile.User.Email,
                Profile = new ProfileClientModel
                {
                    PhoneNumber = userWithProfile.Profile.PhoneNumber,
                    Birthday = userWithProfile.Profile.Birthday
                }
            };
        }

        [HttpPost("logout")]
        public async Task LogoutAsync()
        {
            string token = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            await userApiProxy.LogoutAsync(token);
        }
    }
}