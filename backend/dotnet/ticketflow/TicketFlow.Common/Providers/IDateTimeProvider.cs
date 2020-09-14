using System;

namespace TicketFlow.Common.Providers
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentUtcDateTime();
    }
}