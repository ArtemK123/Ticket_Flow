﻿using Microsoft.AspNetCore.Mvc;

namespace TicketFlow.ApiGateway.Api.Controllers
{
    public class HomeApiController : ControllerBase
    {
        [HttpGet("/")]
        public string Home()
        {
            return "Hello from TicketFlow backend";
        }
    }
}