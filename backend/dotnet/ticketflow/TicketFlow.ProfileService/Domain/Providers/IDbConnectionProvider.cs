using System.Data.Common;

namespace ProfileService.Domain
{
    internal interface IDbConnectionProvider
    {
        DbConnection Get();
    }
}