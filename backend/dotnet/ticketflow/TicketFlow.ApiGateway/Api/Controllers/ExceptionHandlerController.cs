using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.ApiGateway.Models.Exceptions;

namespace TicketFlow.ApiGateway.Api.Controllers
{
    [Route("/error")]
    public class ExceptionHandlerController : ControllerBase
    {
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            if (exception == null || exception is NotFoundException)
            {
                return new NotFoundResult();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}