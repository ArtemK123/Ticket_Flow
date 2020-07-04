using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProfileService.Models;
using ProfileService.Service;

namespace ProfileService.Api.Controllers
{
    [ApiController]
    [Route("/profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly IConfiguration configuration;

        public ProfilesController(IProfileService profileService, IConfiguration configuration)
        {
            this.profileService = profileService;
            this.configuration = configuration;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            var host = configuration["Database:Host"];
            var name = configuration["Database:Name"];
            var user = configuration["Database:User"];
            var password = configuration["Database:Password"];
            var message = $"host: {host}\n name:{name}\n user:{user}\n password:{password}";

            // return new OkObjectResult("Hello from Profiles Api");
            return new OkObjectResult(message);
        }

        [HttpGet("by-id/{id}")]
        public Profile GetById(int id)
        {
            return profileService.GetById(id);
        }

        [HttpPost("by-user")]
        public Profile GetById([FromBody] string userEmail)
        {
            return profileService.GetByUserEmail(userEmail);
        }

        [HttpPost]
        public string Add([FromBody] Profile profile)
        {
            int addedProfileId = profileService.Add(profile);
            return $"Added successfully. Id - {addedProfileId}";
        }
    }
}