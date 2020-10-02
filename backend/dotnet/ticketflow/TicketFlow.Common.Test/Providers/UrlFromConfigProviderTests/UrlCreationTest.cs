using System.Collections.Generic;
using Xunit;

namespace TicketFlow.Common.Test.Providers.UrlFromConfigProviderTests
{
    public class UrlCreationTest : UrlFromConfigProviderTestBase
    {
        [Fact]
        public void GetUrl_BaseUrlAndPortAreValid_ShouldCreateValidUrl()
        {
            var settings = new Dictionary<string, string>
            {
                { "TicketFlow:ServiceBaseUrl", ServiceBaseUrl },
                { "TicketFlow:RunOnRandomPort", false.ToString() },
                { "TicketFlow:Port", Port.ToString() }
            };

            RunGetUrlTest(settings, url =>
            {
                var expectedUrl = $"{ServiceBaseUrl}:{Port}";
                Assert.Equal(expectedUrl, url);
            });
        }
    }
}