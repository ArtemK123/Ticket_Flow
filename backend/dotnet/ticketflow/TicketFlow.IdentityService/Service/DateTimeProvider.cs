using System;

namespace TicketFlow.IdentityService.Service
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentUtcDateTime() => DateTime.UtcNow;
    }
}