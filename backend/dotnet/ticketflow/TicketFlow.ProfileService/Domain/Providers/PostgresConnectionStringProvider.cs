using Microsoft.Extensions.Configuration;

namespace TicketFlow.ProfileService.Domain.Providers
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
            string host = configuration["Database:Host"];
            string databaseName = configuration["Database:Name"];
            string user = configuration["Database:User"];
            string password = configuration["Database:Password"];

            return $"Host={host};Username={user};Password={password};Database={databaseName}";
        }
    }
}