using System;

namespace TicketFlow.IdentityService.Service
{
    internal interface IDateTimeProvider
    {
        DateTime GetCurrentUtcDateTime();
    }
}