using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TicketFlow.Common.WebApi
{
    public abstract class ExceptionHandlerControllerBase : ControllerBase
    {
        private readonly ILogger logger;

        protected ExceptionHandlerControllerBase(ILogger logger)
        {
            this.logger = logger;
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

            IReadOnlyDictionary<Type, Func<Exception, HttpContext, IActionResult>> exceptionMappings = GetAllowedExceptionMappings();
            if (exceptionMappings.TryGetValue(exception.GetType(), out Func<Exception, HttpContext, IActionResult> exceptionMapping))
            {
                logger.LogError(exception.Message, "Error handled by exception handler");
                return exceptionMapping(exception, HttpContext);
            }

            logger.LogCritical(exception, "Internal server error");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        protected abstract IReadOnlyDictionary<Type, Func<Exception, HttpContext, IActionResult>> GetAllowedExceptionMappings();
    }
}