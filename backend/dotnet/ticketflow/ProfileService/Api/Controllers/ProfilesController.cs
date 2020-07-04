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
            return new OkObjectResult("Hello from Profiles Api");
        }

        [HttpGet("by-id/{id}")]
        public Profile GetById([FromRoute] int id)
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