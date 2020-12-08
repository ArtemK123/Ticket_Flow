using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TicketFlow.TicketService.Persistence;
using Xunit;

namespace TicketFlow.TicketService.IntegrationTest
{
    public class TicketApiTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;

        public TicketApiTest(WebApplicationFactory<Startup> webApplicationFactory)
        {
            this.webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task TestAsync()
        {
            HttpClient apiClient = webApplicationFactory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddTransient(serviceProvider => Substitute.For<IMigrationRunner>());
                        services.AddTransient(serviceProvider => Substitute.For<ITicketRepository>());
                    });
                })
                .CreateClient();

            HttpResponseMessage response = await apiClient.GetAsync("/");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}