using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Readers;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Domain.Models;
using TicketFlow.IdentityService.Service;
using TicketFlow.IdentityService.Service.Serializers;
using TicketFlow.IdentityService.WebApi.ClientModels.Requests;

namespace TicketFlow.IdentityService.WebApi.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IStringFromStreamReader stringFromStreamReader;
        private readonly IUserSerializer userSerializer;

        public UsersApiController(IUserService userService, IStringFromStreamReader stringFromStreamReader, IUserSerializer userSerializer)
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
        public string Login([FromBody] LoginRequest loginRequest)
        {
            string token = userService.Login(loginRequest);
            return token;
        }

        [HttpPost("register")]
        public string Register([FromBody] RegisterRequest registerRequest)
        {
            userService.Register(registerRequest);
            return $"Registered successfully user with email={registerRequest.Email}";
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