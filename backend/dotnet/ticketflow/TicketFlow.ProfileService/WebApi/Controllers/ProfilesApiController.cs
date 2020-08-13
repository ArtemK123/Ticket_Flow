using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Readers;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Factories;
using TicketFlow.ProfileService.Client.Extensibility.Models;
using TicketFlow.ProfileService.Client.Extensibility.Serializers;
using TicketFlow.ProfileService.Service;
using TicketFlow.ProfileService.WebApi.ClientModels.Requests;

namespace TicketFlow.ProfileService.WebApi.Controllers
{
    [ApiController]
    [Route("/profiles")]
    public class ProfilesApiController : ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly IStringFromStreamReader stringFromStreamReader;
        private readonly IProfileSerializer profileSerializer;
        private readonly IProfileFactory profileFactory;

        public ProfilesApiController(IProfileService profileService, IStringFromStreamReader stringFromStreamReader, IProfileSerializer profileSerializer, IProfileFactory profileFactory)
        {
            this.profileService = profileService;
            this.stringFromStreamReader = stringFromStreamReader;
            this.profileSerializer = profileSerializer;
            this.profileFactory = profileFactory;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return new OkObjectResult("Hello from Profile service API");
        }

        [HttpGet("by-id/{id}")]
        public ProfileSerializationModel GetById([FromRoute] int id)
        {
            IProfile profile = profileService.GetById(id);
            return profileSerializer.Serialize(profile);
        }

        [HttpPost("by-user")]
        public async Task<ProfileSerializationModel> GetByUser()
        {
            string userEmail = await stringFromStreamReader.ReadAsync(Request.Body, Encoding.UTF8);
            IProfile profile = profileService.GetByUserEmail(userEmail);
            return profileSerializer.Serialize(profile);
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddProfileRequest addProfileRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            IProfile addedProfile = profileFactory.Create(new ProfileCreationModel(addProfileRequest.UserEmail, addProfileRequest.PhoneNumber, addProfileRequest.Birthday));
            profileService.Add(addedProfile);
            var createdEntityUri = new Uri(new Uri(GetAppBaseUrl(Request)), Url.Action(nameof(GetById), new { id = addedProfile.Id }));
            return new CreatedResult(createdEntityUri, addedProfile);
        }

        private static string GetAppBaseUrl(HttpRequest request) => $"{request.Scheme}://{request.Host}{request.PathBase}";
    }
}