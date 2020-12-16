using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using Xunit;

namespace TicketFlow.MovieService.IntegrationTest.ApiTests
{
    public class CinemaHallApiViaProxyTest
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly ICinemaHallApiProxy proxy;

        public CinemaHallApiViaProxyTest()
        {
            webApplicationFactory = new WebApplicationFactoryProvider().GetWebApplicationFactory();
            proxy = webApplicationFactory.Services.GetService<ICinemaHallApiProxy>();
        }

        [Fact]
        public async Task Test()
        {

        }
    }
}