using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketFlow.Common.WebApi;
using TicketFlow.Common.WebApi.Handlers;
using TicketFlow.ProfileService.Client.Extensibility.Exceptions;

namespace TicketFlow.ProfileService.WebApi.Controllers
{
    [Route("/error")]
    public class ExceptionHandlerController : ExceptionHandlerControllerBase
    {
        private readonly IExceptionHeaderHandler exceptionHeaderHandler;

        public ExceptionHandlerController(ILoggerFactory loggerFactory, IExceptionHeaderHandler exceptionHeaderHandler)
            : base(loggerFactory.CreateLogger(nameof(ExceptionHandlerController)))
        {
            this.exceptionHeaderHandler = exceptionHeaderHandler;
        }

        protected override IReadOnlyDictionary<Type, Func<Exception, HttpContext, IActionResult>> GetAllowedExceptionMappings()
            => new Dictionary<Type, Func<Exception, HttpContext, IActionResult>>
        {
            { typeof(ProfileNotFoundByIdException), HandleNotFoundException },
            { typeof(ProfileNotFoundByUserEmailException), HandleNotFoundException },
            {
                typeof(ProfileNotFoundByUserEmailException), (exception, context) =>
                {
                    exceptionHeaderHandler.WriteExceptionHeader(context.Response.Headers, exception);
                    return new BadRequestObjectResult(exception.Message);
                }
            }
        };

        private IActionResult HandleNotFoundException<TException>(TException exception, HttpContext httpContext)
            where TException : Exception
        {
            exceptionHeaderHandler.WriteExceptionHeader(httpContext.Response.Headers, exception);
            return new ContentResult { StatusCode = (int)HttpStatusCode.NotFound, Content = exception.Message };
        }
    }
}