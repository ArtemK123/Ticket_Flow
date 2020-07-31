using System.Data.Common;

namespace TicketFlow.ProfileService.Domain.Providers
{
    internal interface IPosgtresDbConnectionProvider
    {
        DbConnection Get();
    }
}