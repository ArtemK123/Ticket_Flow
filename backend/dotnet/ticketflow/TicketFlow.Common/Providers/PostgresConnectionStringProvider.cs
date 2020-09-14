using Microsoft.Extensions.Configuration;

namespace TicketFlow.Common.Providers
{
    internal class PostgresConnectionStringProvider : IPostgresConnectionStringProvider
    {
        private readonly IConfiguration configuration;

        public PostgresConnectionStringProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetConnectionString()
        {
            string host = configuration.GetValue<string>("Database:Host");
            string databaseName = configuration.GetValue<string>("Database:Name");
            string user = configuration.GetValue<string>("Database:User");
            string password = configuration.GetValue<string>("Database:Password");

            return $"Host={host};Username={user};Password={password};Database={databaseName}";
        }
    }
}