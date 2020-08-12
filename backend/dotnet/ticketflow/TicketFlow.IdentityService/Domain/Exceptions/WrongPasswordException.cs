﻿using System;

namespace TicketFlow.IdentityService.Domain.Exceptions
{
    internal class WrongPasswordException : Exception
    {
        public WrongPasswordException(string message)
            : base(message)
        {
        }
    }
}