using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.Providers;
using Xunit;

namespace TicketFlow.Common.Test.Providers.UrlFromConfigProviderTest
{
    public class RunOnRandomPortSettingTest
    {
        private const string ServiceBaseUrl = "http://localhost";
        private const int Port = 12345;

        private readonly UrlFromConfigProvider urlFromConfigProvider;

        public RunOnRandomPortSettingTest()
        {
            urlFromConfigProvider = new UrlFromConfigProvider();
        }

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

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            Assert.Throws<InvalidOperationException>(() => urlFromConfigProvider.GetUrl(configuration));
        }

        private static int GetPortFromUrl(string url) => new Uri(url).Port;

        private void RunGetUrlTest(IReadOnlyDictionary<string, string> settings, Action<string> assertAction)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            string actual = urlFromConfigProvider.GetUrl(configuration);

            assertAction(actual);
        }
    }
}