using System.Threading.Tasks;
using NSubstitute;
using TicketFlow.Common.ServiceUrl.Providers;
using TicketFlow.IdentityService.Client.Providers;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Providers
{
    public class IdentityServiceUrlProviderTest
    {
        private const string ServiceName = "IdentityService";
        private const string Url = "http://localhost";

        private readonly IServiceUrlProvider serviceUrlProviderMock;

        private readonly IdentityServiceUrlProvider identityServiceUrlProvider;

        public IdentityServiceUrlProviderTest()
        {
            serviceUrlProviderMock = Substitute.For<IServiceUrlProvider>();
            identityServiceUrlProvider = new IdentityServiceUrlProvider(serviceUrlProviderMock);
        }

        [Fact]
        internal async Task GetUrlAsync_ShouldGetUrlFromProvider()
        {
            serviceUrlProviderMock.GetUrlAsync(ServiceName).Returns(Task.FromResult(Url));

            var actualUrl = await identityServiceUrlProvider.GetUrlAsync();

            Assert.Equal(Url, actualUrl);
        }
    }
}