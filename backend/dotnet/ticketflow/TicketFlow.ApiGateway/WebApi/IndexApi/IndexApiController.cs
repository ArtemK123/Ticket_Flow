using Microsoft.AspNetCore.Mvc;

namespace TicketFlow.ApiGateway.WebApi.IndexApi
{
    [ApiController]
    public class IndexApiController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return new OkObjectResult("Hello from TicketFlow API");
        }
    }
}