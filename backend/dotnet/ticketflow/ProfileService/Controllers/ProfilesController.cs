using Microsoft.AspNetCore.Mvc;

namespace ProfileService.Controllers
{
    [ApiController]
    [Route("/profiles")]
    public class ProfilesController : ControllerBase
    {
        [HttpGet]
        [HttpGet("/")]
        public string Get()
        {
            return "Hello world";
        }
    }
}