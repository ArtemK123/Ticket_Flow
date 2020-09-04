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
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Factories;
using TicketFlow.ProfileService.Client.Extensibility.Models;

namespace TicketFlow.ApiGateway.WebApi.UserApi
{
    [ApiController]
    [Route("/")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserFactory userFactory;
        private readonly IProfileFactory profileFactory;
        private readonly IStringFromStreamReader stringFromStreamReader;

        public UserApiController(IUserService userService, IUserFactory userFactory, IProfileFactory profileFactory, IStringFromStreamReader stringFromStreamReader)
        {
            this.userService = userService;
            this.userFactory = userFactory;
            this.profileFactory = profileFactory;
            this.stringFromStreamReader = stringFromStreamReader;
        }

        [HttpPost("login")]
        public string Login([FromBody] LoginRequestApiModel loginRequestApiModel)
        {
            return userService.Login(new LoginRequest(loginRequestApiModel.Email, loginRequestApiModel.Password));
        }

        [HttpPost("register")]
        public string Register([FromBody] RegistrationRequestApiModel registrationRequest)
        {
            IUser user = userFactory.Create(new UserCreationModel(registrationRequest.Email, registrationRequest.Password, Role.User));
            IProfile profile = profileFactory.Create(new ProfileCreationModel(registrationRequest.Email, registrationRequest.Profile.PhoneNumber, registrationRequest.Profile.Birthday));

            return userService.Register(new UserWithProfile(user, profile));
        }

        [HttpPost("profile")]
        public async Task<GetProfileResponse> GetProfileAsync()
        {
            string token = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            UserWithProfile userWithProfile = userService.GetUserWithProfile(token);
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
        public async Task<string> LogoutAsync()
        {
            string token = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            userService.Logout(token);
            return "Logout is successful";
        }
    }
}