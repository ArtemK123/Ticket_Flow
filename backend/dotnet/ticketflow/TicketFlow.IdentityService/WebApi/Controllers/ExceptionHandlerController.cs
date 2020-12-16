using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketFlow.Common.WebApi;
using TicketFlow.Common.WebApi.Handlers;
using TicketFlow.IdentityService.Client.Extensibility.Exceptions;

namespace TicketFlow.IdentityService.WebApi.Controllers
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
            { typeof(UserNotFoundByTokenException), HandleNotFoundException },
            { typeof(UserNotFoundByEmailException), HandleNotFoundException },
            { typeof(WrongPasswordException), (exception, _) => new ContentResult { StatusCode = (int)HttpStatusCode.Unauthorized, Content = exception.Message } },
            { typeof(UserNotUniqueException), (exception, _) => new BadRequestObjectResult(exception.Message) }
        };

        private IActionResult HandleNotFoundException<TException>(TException exception, HttpContext httpContext)
            where TException : Exception
        {
            exceptionHeaderHandler.WriteExceptionHeader(httpContext.Response.Headers, exception);
            return new ContentResult { StatusCode = (int)HttpStatusCode.NotFound, Content = exception.Message };
        }
    }
}