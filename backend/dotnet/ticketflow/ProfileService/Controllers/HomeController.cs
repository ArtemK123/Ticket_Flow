using Microsoft.AspNetCore.Mvc;

namespace ProfileService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [HttpGet("/")]
        public string Get()
        {
            return "Hello world";
        }
    }
}