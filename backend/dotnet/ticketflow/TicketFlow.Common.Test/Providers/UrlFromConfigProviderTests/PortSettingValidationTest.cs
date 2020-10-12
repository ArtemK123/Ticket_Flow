using System;
using System.Collections.Generic;
using Xunit;

namespace TicketFlow.Common.Test.Providers.UrlFromConfigProviderTests
{
    public class PortSettingValidationTest : UrlFromConfigProviderTestBase
    {
        private const int MinimumPortValue = 1024;
        private const int MaximumPortValue = 64000;
        private const int DefaultIntValue = default;

        private const string PortLowerThanAllowedExceptionMessage = "Application port is wrongly configured. Port value is lower than allowed 1024";
        private const string PortHigherThanAllowedExceptionMessage = "Application port is wrongly configured. Port value is higher than allowed 64000";

        [Theory]
        [InlineData(MinimumPortValue)]
        [InlineData(MaximumPortValue)]
        internal void GetUrl_GetPortSetting_ValidPort_ShouldUseGivenPort(int port)
        {
            var settings = new Dictionary<string, string>
            {
                { "TicketFlow:ServiceBaseUrl", ServiceBaseUrl },
                { "TicketFlow:RunOnRandomPort", false.ToString() },
                { "TicketFlow:Port", port.ToString() }
            };

            RunGetUrlTest(settings, url =>
            {
                Assert.Equal(port, GetPortFromUrl(url));
            });
        }

        [Theory]
        [InlineData(DefaultIntValue, "Application port is wrongly configured. Please, setup configuration file with TicketFlow:Port or set TicketFlow:RunOnRandomPort=true")]
        [InlineData(MinimumPortValue - 1, PortLowerThanAllowedExceptionMessage)]
        [InlineData(MaximumPortValue + 1, PortHigherThanAllowedExceptionMessage)]
        internal void GetUrl_GetPortSetting_InvalidPort_ShouldThrowException(int port, string exceptionMessage)
        {
            var settings = new Dictionary<string, string>
            {
                { "TicketFlow:ServiceBaseUrl", ServiceBaseUrl },
                { "TicketFlow:RunOnRandomPort", false.ToString() },
                { "TicketFlow:Port", port.ToString() }
            };

            RunGetUrlThrowsExceptionTest<Exception>(settings, exception => Assert.Equal(exceptionMessage, exception.Message));
        }
    }
}