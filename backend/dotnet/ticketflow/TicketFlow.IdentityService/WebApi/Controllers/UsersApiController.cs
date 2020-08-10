using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Readers;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Service;
using TicketFlow.IdentityService.WebApi.ClientModels.Requests;
using TicketFlow.IdentityService.WebApi.ClientModels.Responses;
using TicketFlow.IdentityService.WebApi.Converters;

namespace TicketFlow.IdentityService.WebApi.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IStringFromStreamReader stringFromStreamReader;
        private readonly IUserWebConverter userWebConverter;

        public UsersApiController(IUserService userService, IStringFromStreamReader stringFromStreamReader, IUserWebConverter userWebConverter)
        {
            this.userService = userService;
            this.stringFromStreamReader = stringFromStreamReader;
            this.userWebConverter = userWebConverter;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return new OkObjectResult("Hello from Identity service API");
        }

        [HttpPost("getByToken")]
        public async Task<UserWebModel> GetByToken()
        {
            string token = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            IAuthorizedUser authorizedUser = userService.GetByToken(token);
            return userWebConverter.Convert(authorizedUser);
        }

        [HttpPost("getByEmail")]
        public async Task<UserWebModel> GetByEmail()
        {
            string email = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            IUser user = userService.GetByEmail(email);
            return user is IAuthorizedUser authorizedUser ? userWebConverter.Convert(authorizedUser) : userWebConverter.Convert(user);
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