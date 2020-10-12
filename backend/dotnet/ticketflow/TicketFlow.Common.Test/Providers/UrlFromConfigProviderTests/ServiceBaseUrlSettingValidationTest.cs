using System;
using System.Collections.Generic;
using Xunit;

namespace TicketFlow.Common.Test.Providers.UrlFromConfigProviderTests
{
    public class ServiceBaseUrlSettingValidationTest : UrlFromConfigProviderTestBase
    {
        private const string ServiceBaseUrlInvalidExceptionMessage = "Application port is wrongly configured. Please, specify TicketFlow:ServiceBaseUrl in configuration file";

        [Fact]
        internal void GetUrl_GetServiceBaseUrlSetting_ValidSetting_ShouldUseGivenServiceBaseUrl()
        {
            var settings = new Dictionary<string, string>
            {
                { "TicketFlow:ServiceBaseUrl", ServiceBaseUrl },
                { "TicketFlow:RunOnRandomPort", "true" }
            };

            RunGetUrlTest(settings, url =>
            {
                Uri uri = new Uri(url);
                var actualServiceBaseUrl = $"{uri.Scheme}://{uri.Host}";
                Assert.Equal(ServiceBaseUrl, actualServiceBaseUrl);
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        internal void GetUrl_GetServiceBaseUrlSetting_SettingIsEmptyOrNull_ShouldThrowException(string serviceBaseUrl)
        {
            var settings = new Dictionary<string, string>
            {
                { "TicketFlow:ServiceBaseUrl", serviceBaseUrl },
                { "TicketFlow:RunOnRandomPort", "true" }
            };

            RunGetUrlThrowsExceptionTest<Exception>(settings, exception =>
            {
                Assert.Equal(ServiceBaseUrlInvalidExceptionMessage, exception.Message);
            });
        }
    }
}