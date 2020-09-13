using Microsoft.Extensions.Configuration;
using TicketFlow.Common.Providers;
using Xunit;

namespace TicketFlow.Common.Test.Providers.PostgresConnectionStringProviderTest
{
    public class PostgresConnectionStringProviderTest
    {
        private readonly PostgresConnectionStringProvider postgresConnectionStringProvider;

        public PostgresConnectionStringProviderTest()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("Providers\\PostgresConnectionStringProviderTest\\testSettings.json")
                .Build();

            postgresConnectionStringProvider = new PostgresConnectionStringProvider(config);
        }

        [Fact]
        public void GetConnectionString_ShouldReadValuesFromConfiguration()
        {
            string expectedHost = "host";
            string expectedDatabaseName = "databaseName";
            string expectedUser = "user";
            string expectedPassword = "password";
            string expectedConnectionString = $"Host={expectedHost};Username={expectedUser};Password={expectedPassword};Database={expectedDatabaseName}";

            string actualConnectionString = postgresConnectionStringProvider.GetConnectionString();
            Assert.Equal(expectedConnectionString, actualConnectionString);
        }
    }
}