using System.Data.Common;
using Npgsql;

namespace TicketFlow.ProfileService.Domain.Providers
{
    internal class PostgresDbConnectionProvider : IPosgtresDbConnectionProvider
    {
        private readonly IPostgresConnectionStringProvider connectionStringProvider;

        public PostgresDbConnectionProvider(IPostgresConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public DbConnection Get() => new NpgsqlConnection(connectionStringProvider.GetConnectionString());
    }
}