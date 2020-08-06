using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketFlow.IdentityService.Service.Entities.Exceptions;

namespace TicketFlow.IdentityService.Api.Controllers
{
    [Route("/error")]
    public class ExceptionHandlerController : ControllerBase
    {
        private readonly ILogger logger;

        public ExceptionHandlerController(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger(nameof(ExceptionHandlerController));
        }

        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            logger.LogError(exception, "Error handled by exception handler");

            if (exception == null || exception is NotFoundException)
            {
                return new NotFoundResult();
            }

            if (exception is WrongPasswordException)
            {
                return new UnauthorizedResult();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}