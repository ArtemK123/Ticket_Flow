using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.TicketService.Domain.Exceptions;

namespace TicketFlow.TicketService.WebApi.Controllers
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

            if (exception is TicketAlreadyOrderedException)
            {
                return new BadRequestObjectResult(exception.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}