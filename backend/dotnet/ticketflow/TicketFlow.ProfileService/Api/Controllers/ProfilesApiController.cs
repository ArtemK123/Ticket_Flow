using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Readers;
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
        private readonly IStringFromStreamReader stringFromStreamReader;

        public ProfilesApiController(IProfileService profileService, IStringFromStreamReader stringFromStreamReader)
        {
            this.profileService = profileService;
            this.stringFromStreamReader = stringFromStreamReader;
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
            string userEmail = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
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

        private static Profile Convert(ProfileClientModel clientModel)
            => new Profile(clientModel.UserEmail, clientModel.PhoneNumber, clientModel.Birthday);

        private static string GetAppBaseUrl(HttpRequest request) => $"{request.Scheme}://{request.Host}{request.PathBase}";
    }
}