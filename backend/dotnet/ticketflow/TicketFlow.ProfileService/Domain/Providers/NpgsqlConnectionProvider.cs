using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ProfileService.Domain
{
    internal class NpgsqlConnectionProvider : IDbConnectionProvider
    {
        private readonly IConfiguration configuration;

        public NpgsqlConnectionProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbConnection Get()
        {
            string host = configuration["Database:Host"];
            string databaseName = configuration["Database:Name"];
            string user = configuration["Database:User"];
            string password = configuration["Database:Password"];

            return new NpgsqlConnection($"Host={host};Username={user};Password={password};Database={databaseName}");
        }
    }
}