using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<Profile> GetByUser()
        {
            string userEmail = await GetStringFromStreamAsync(Request.Body, Encoding.UTF8);
            return profileService.GetByUserEmail(userEmail);
        }

        [HttpPost]
        public string Add([FromBody] Profile profile)
        {
            int addedProfileId = profileService.Add(profile);
            return $"Added successfully. Id - {addedProfileId}";
        }

        private async Task<string> GetStringFromStreamAsync(Stream stream, Encoding encoding)
        {
            using var reader = new StreamReader(stream, encoding);
            return await reader.ReadToEndAsync();
        }
    }
}