using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketFlow.ProfileService.Models.Exceptions;

namespace TicketFlow.ProfileService.Api.Controllers
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

            if (exception == null)
            {
                logger.LogError("Executed without exception. Probably, direct request to /error page");
                return new NotFoundResult();
            }

            logger.LogError(exception.Message, "Error handled by exception handler");
            if (exception is NotFoundException)
            {
                return new NotFoundResult();
            }

            logger.LogError(exception, "Internal server error");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}