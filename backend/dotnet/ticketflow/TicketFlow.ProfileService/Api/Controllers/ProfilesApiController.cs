using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.ProfileService.Api.ClientModels;
using TicketFlow.ProfileService.Models;
using TicketFlow.ProfileService.Service;

namespace TicketFlow.ProfileService.Api.Controllers
{
    [ApiController]
    [Route("/profiles")]
    public class ProfilesApiController : ControllerBase
    {
        private readonly IProfileService profileService;

        public ProfilesApiController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return new OkObjectResult("Hello from Profile service API");
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
        public IActionResult Add([FromBody] ProfileClientModel profileClientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Profile addedProfile = profileService.Add(Convert(profileClientModel));
            var createdEntityUri = new Uri(new Uri(GetAppBaseUrl(Request)), Url.Action(nameof(GetById), new { id = addedProfile.Id }));
            return new CreatedResult(createdEntityUri, addedProfile);
        }

        private static async Task<string> GetStringFromStreamAsync(Stream stream, Encoding encoding)
        {
            using var reader = new StreamReader(stream, encoding);
            return await reader.ReadToEndAsync();
        }

        private static Profile Convert(ProfileClientModel clientModel)
            => new Profile(clientModel.UserEmail, clientModel.PhoneNumber, clientModel.Birthday);

        private static string GetAppBaseUrl(HttpRequest request) => $"{request.Scheme}://{request.Host}{request.PathBase}";
    }
}