using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ApiGateway.Models.ApiGateway.UserApi;
using ApiGateway.Models.IdentityService;
using ApiGateway.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Api.Controllers
{
    public class UserApiController : ControllerBase
    {
        private readonly IUserService userService;

        public UserApiController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            userService.Register(registerRequest);
            return new OkResult();
        }

        [HttpPost("profile")]
        public async Task<IActionResult> GetProfile()
        {
            string tokenFromBody = await GetStringFromStreamAsync();
            GetProfileResponse getProfileResponse = userService.GetProfile(tokenFromBody);
            return new OkObjectResult(getProfileResponse);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            throw new NotImplementedException();
        }

        private async Task<string> GetStringFromStreamAsync()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }
    }
}