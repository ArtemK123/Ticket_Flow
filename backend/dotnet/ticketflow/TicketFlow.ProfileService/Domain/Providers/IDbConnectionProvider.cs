using System.Data.Common;

namespace TicketFlow.ProfileService.Domain
{
    internal interface IDbConnectionProvider
    {
        DbConnection Get();
    }
}