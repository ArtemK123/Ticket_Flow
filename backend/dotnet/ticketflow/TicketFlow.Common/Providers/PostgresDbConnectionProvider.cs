using System.Data.Common;
using Npgsql;

namespace TicketFlow.Common.Providers
{
    internal class PostgresDbConnectionProvider : IPostgresDbConnectionProvider
    {
        private readonly IPostgresConnectionStringProvider connectionStringProvider;

        public PostgresDbConnectionProvider(IPostgresConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public DbConnection Get() => new NpgsqlConnection(connectionStringProvider.GetConnectionString());
    }
}