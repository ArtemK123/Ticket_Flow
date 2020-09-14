using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Readers;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Serializers;
using TicketFlow.IdentityService.Service;
using TicketFlow.IdentityService.WebApi.ClientModels.Requests;

namespace TicketFlow.IdentityService.WebApi.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IStringFromStreamReader stringFromStreamReader;
        private readonly IUserSerializer userSerializer;

        public UserApiController(IUserService userService, IStringFromStreamReader stringFromStreamReader, IUserSerializer userSerializer)
        {
            this.userService = userService;
            this.stringFromStreamReader = stringFromStreamReader;
            this.userSerializer = userSerializer;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return new OkObjectResult("Hello from Identity service API");
        }

        [HttpPost("getByToken")]
        public async Task<UserSerializationModel> GetByToken()
        {
            string token = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            IAuthorizedUser authorizedUser = userService.GetByToken(token);
            return userSerializer.Serialize(authorizedUser);
        }

        [HttpPost("getByEmail")]
        public async Task<UserSerializationModel> GetByEmail()
        {
            string email = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            IUser user = userService.GetByEmail(email);
            return userSerializer.Serialize(user);
        }

        [HttpPost("login")]
        public string Login([FromBody] LoginRequestApiModel loginRequestApiModel)
        {
            string token = userService.Login(new LoginRequest(loginRequestApiModel.Email, loginRequestApiModel.Password));
            return token;
        }

        [HttpPost("register")]
        public string Register([FromBody] RegisterRequestApiModel registerRequestApiModel)
        {
            userService.Register(new RegisterRequest(registerRequestApiModel.Email, registerRequestApiModel.Password));
            return $"Registered successfully user with email={registerRequestApiModel.Email}";
        }

        [HttpPost("logout")]
        public async Task<string> Logout()
        {
            string token = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            userService.Logout(token);
            return "Logout successful";
        }
    }
}