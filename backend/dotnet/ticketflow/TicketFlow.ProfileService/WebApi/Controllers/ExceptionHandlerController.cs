using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.WebApi;
using TicketFlow.Common.WebApi.Handlers;

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
            { typeof(NotFoundException), (_, __) => new NotFoundResult() },
            {
                typeof(NotUniqueEntityException), (exception, context) =>
                {
                    exceptionHeaderHandler.WriteExceptionHeader(context.Response.Headers, exception);
                    return new BadRequestObjectResult(exception.Message);
                }
            }
        };
    }
}