﻿namespace TicketFlow.IdentityService.WebApi.ClientModels.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}