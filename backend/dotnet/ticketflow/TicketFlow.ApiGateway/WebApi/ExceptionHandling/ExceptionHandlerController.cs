using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.WebApi;
using TicketFlow.IdentityService.Client.Extensibility.Exceptions;
using TicketFlow.TicketService.Client.Extensibility.Exceptions;

namespace TicketFlow.ApiGateway.WebApi.ExceptionHandling
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public class ExceptionHandlerController : ExceptionHandlerControllerBase
    {
        public ExceptionHandlerController(ILoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger(nameof(ExceptionHandlerController)))
        {
        }

        protected override IReadOnlyDictionary<Type, Func<Exception, HttpContext, IActionResult>> GetAllowedExceptionMappings()
            => new Dictionary<Type, Func<Exception, HttpContext, IActionResult>>
        {
            { typeof(NotFoundException), (_, __) => new NotFoundResult() },
            { typeof(WrongPasswordException), (_, __) => new UnauthorizedResult() },
            { typeof(NotUniqueEntityException), (exception, _) => new BadRequestObjectResult(exception.Message) },
            { typeof(TicketAlreadyOrderedException), (exception, _) => new BadRequestObjectResult(exception.Message) }
        };
    }
}