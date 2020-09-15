using System.Data.Common;
using Npgsql;
using NSubstitute;
using TicketFlow.Common.Providers;
using Xunit;

namespace TicketFlow.Common.Test.Providers
{
    public class PostgresDbConnectionProviderTest
    {
        private const string ConnectionString = "Host=host;Username=user;Password=password;Database=databaseName";

        private readonly IPostgresConnectionStringProvider postgresConnectionStringProviderMock;
        private readonly PostgresDbConnectionProvider postgresDbConnectionProvider;

        public PostgresDbConnectionProviderTest()
        {
            postgresConnectionStringProviderMock = CreateMockedPostgresConnectionStringProvider();
            postgresDbConnectionProvider = new PostgresDbConnectionProvider(postgresConnectionStringProviderMock);
        }

        [Fact]
        public void Get_ShouldReturnNpgsqlConnection()
        {
            DbConnection actual = postgresDbConnectionProvider.Get();

            Assert.IsType<NpgsqlConnection>(actual);
        }

        [Fact]
        public void Get_WhenReturnNpgsqlConnection_ShouldTakeConnectionStringFromProvider()
        {
            string actualConnectionString = postgresDbConnectionProvider.Get().ConnectionString;

            Assert.Equal(ConnectionString, actualConnectionString);
        }

        [Fact]
        public void Get_ShouldCallPostgresConnectionStringProvider()
        {
            postgresDbConnectionProvider.Get();

            postgresConnectionStringProviderMock.Received().GetConnectionString();
        }

        private IPostgresConnectionStringProvider CreateMockedPostgresConnectionStringProvider()
        {
            var mock = Substitute.For<IPostgresConnectionStringProvider>();
            mock.GetConnectionString().Returns(ConnectionString);
            return mock;
        }
    }
}