using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.IdentityService.Entities;
using TicketFlow.IdentityService.Service;
using TicketFlow.IdentityService.WebApi.ClientModels.Requests;

namespace TicketFlow.IdentityService.WebApi.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersApiController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return new OkObjectResult("Hello from Identity service API");
        }

        [HttpPost("getByToken")]
        public async Task<User> GetByToken()
        {
            string token = await GetStringFromStreamAsync(Request.Body, Encoding.UTF8);
            User user = userService.GetByToken(token);
            return user;
        }

        [HttpPost("getByEmail")]
        public async Task<User> GetByEmail()
        {
            string email = await GetStringFromStreamAsync(Request.Body, Encoding.UTF8);
            User user = userService.GetByEmail(email);
            return user;
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
            string token = await GetStringFromStreamAsync(Request.Body, Encoding.UTF8);
            userService.Logout(token);
            return "Logout successful";
        }

        private static async Task<string> GetStringFromStreamAsync(Stream stream, Encoding encoding)
        {
            using var reader = new StreamReader(stream, encoding);
            return await reader.ReadToEndAsync();
        }
    }
}