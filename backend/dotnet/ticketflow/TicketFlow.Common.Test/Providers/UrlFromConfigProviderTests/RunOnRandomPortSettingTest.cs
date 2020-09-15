using System;
using System.Collections.Generic;
using Xunit;

namespace TicketFlow.Common.Test.Providers.UrlFromConfigProviderTests
{
    public class RunOnRandomPortSettingTest : UrlFromConfigProviderTestBase
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetUrl_GetRunOnRandomPortFromSetting_WhenTrue_ShouldGenerateRandomPort(bool runOnRandomPort)
        {
            var settings = new Dictionary<string, string>
            {
                { "TicketFlow:ServiceBaseUrl", ServiceBaseUrl },
                { "TicketFlow:RunOnRandomPort", runOnRandomPort.ToString() },
                { "TicketFlow:Port", Port.ToString() }
            };

            RunGetUrlTest(settings, url =>
            {
                var actualPort = GetPortFromUrl(url);
                if (runOnRandomPort)
                {
                    Assert.NotEqual(Port, actualPort);
                }
                else
                {
                    Assert.Equal(Port, actualPort);
                }
            });
        }

        [Fact]
        public void GetUrl_GetRunOnRandomPortFromSetting_NotFound_ShouldUsePortSetting()
        {
            var settings = new Dictionary<string, string>
            {
                { "TicketFlow:ServiceBaseUrl", ServiceBaseUrl },
                { "TicketFlow:Port", Port.ToString() }
            };

            RunGetUrlTest(settings, url =>
            {
                var actualPort = GetPortFromUrl(url);
                Assert.Equal(Port, actualPort);
            });
        }

        [Fact]
        public void GetUrl_GetRunOnRandomPortFromSetting_Invalid_ShouldThrowException()
        {
            var settings = new Dictionary<string, string>
            {
                { "TicketFlow:ServiceBaseUrl", ServiceBaseUrl },
                { "TicketFlow:RunOnRandomPort", "invalid" },
                { "TicketFlow:Port", Port.ToString() }
            };

            RunGetUrlThrowsExceptionTest<InvalidOperationException>(settings, exception => { });
        }
    }
}