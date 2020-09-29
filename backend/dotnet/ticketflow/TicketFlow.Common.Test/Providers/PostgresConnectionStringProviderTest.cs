using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.Providers;
using Xunit;

namespace TicketFlow.Common.Test.Providers
{
    public class PostgresConnectionStringProviderTest
    {
        [Fact]
        public void GetConnectionString_ShouldReadValuesFromConfiguration()
        {
            string expectedHost = "host";
            string expectedDatabaseName = "databaseName";
            string expectedUser = "user";
            string expectedPassword = "password";
            string expectedConnectionString = $"Host={expectedHost};Username={expectedUser};Password={expectedPassword};Database={expectedDatabaseName}";

            var settings = new Dictionary<string, string>
            {
                { "Database:Host", expectedHost },
                { "Database:Name", expectedDatabaseName },
                { "Database:User", expectedUser },
                { "Database:Password", expectedPassword }
            };

            string actualConnectionString = CreatePostgresConnectionStringProvider(settings).GetConnectionString();
            Assert.Equal(expectedConnectionString, actualConnectionString);
        }

        private IPostgresConnectionStringProvider CreatePostgresConnectionStringProvider(IReadOnlyDictionary<string, string> settings)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            return new PostgresConnectionStringProvider(configuration);
        }
    }
}