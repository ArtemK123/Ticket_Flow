using System.Data.Common;

namespace TicketFlow.Common.Providers
{
    public interface IPostgresDbConnectionProvider
    {
        DbConnection Get();
    }
}