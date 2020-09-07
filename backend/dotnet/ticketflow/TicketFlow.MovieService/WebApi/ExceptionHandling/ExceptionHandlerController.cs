﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.WebApi;

namespace TicketFlow.MovieService.WebApi.ExceptionHandling
{
    [Route("/error")]
    public class ExceptionHandlerController : ExceptionHandlerControllerBase
    {
        public ExceptionHandlerController(ILoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger(nameof(ExceptionHandlerController)))
        {
        }

        protected override IReadOnlyDictionary<Type, Func<Exception, IActionResult>> GetAllowedExceptionMappings() => new Dictionary<Type, Func<Exception, IActionResult>>
        {
            { typeof(NotFoundException), _ => new NotFoundResult() }
        };
    }
}