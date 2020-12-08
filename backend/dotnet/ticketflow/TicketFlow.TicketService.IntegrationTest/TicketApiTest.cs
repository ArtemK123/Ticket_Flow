using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
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
            HttpClient apiClient = webApplicationFactory.CreateClient();

            HttpResponseMessage response = await apiClient.GetAsync("/tickets");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}