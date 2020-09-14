using System;

namespace TicketFlow.Common.Providers
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentUtcDateTime() => DateTime.UtcNow;
    }
}