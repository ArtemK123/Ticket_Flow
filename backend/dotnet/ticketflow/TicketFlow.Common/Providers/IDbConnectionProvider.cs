using System.Data.Common;

namespace TicketFlow.Common.Providers
{
    public interface IDbConnectionProvider
    {
        DbConnection Get();
    }
}