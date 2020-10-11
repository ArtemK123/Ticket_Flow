using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Scenarios;
using Xunit;

namespace TicketFlow.Common.Test.ServiceUrl.Scenarios
{
    public class ServiceUrlFromSettingsScenarioTest
    {
        private const string ServiceName = "Movie";
        private static readonly string UrlSettingPath = $"TicketFlow:{ServiceName}:Url";

        [Fact]
        public void ProvidingType_FromSettings()
        {
            ServiceUrlProvidingType expected = ServiceUrlProvidingType.FromSettings;
            var actual = CreateServiceUrlFromSettingsProvider(new Dictionary<string, string>()).ProvidingType;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetUrl_SettingIsNotDefined_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => CreateServiceUrlFromSettingsProvider(new Dictionary<string, string>()).GetUrl(ServiceName));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetUrl_UrlIsNullOrEmpty_ShouldThrowException(string url)
        {
            var settings = new Dictionary<string, string>
            {
                { UrlSettingPath, url }
            };

            Assert.Throws<Exception>(() => CreateServiceUrlFromSettingsProvider(settings).GetUrl(ServiceName));
        }

        [Fact]
        public void GetUrl_ExceptionMessage_ShouldThrowExceptionWithCorrectMessage()
        {
            var expected = $"Please, specify the url for service {ServiceName} in the configuration file by the path {UrlSettingPath}";

            Exception exception = Assert.Throws<Exception>(() => CreateServiceUrlFromSettingsProvider(new Dictionary<string, string>()).GetUrl(ServiceName));

            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public void GetUrl_ValidUrl_ShouldReturnUrlFromSettings()
        {
            const string expectedUrl = "http://localhost:9000";
            var settings = new Dictionary<string, string>
            {
                { UrlSettingPath, expectedUrl }
            };

            string actualUrl = CreateServiceUrlFromSettingsProvider(settings).GetUrl(ServiceName);

            Assert.Equal(expectedUrl, actualUrl);
        }

        private static ServiceUrlFromSettingsScenario CreateServiceUrlFromSettingsProvider(IReadOnlyDictionary<string, string> settings)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            return new ServiceUrlFromSettingsScenario(configuration);
        }
    }
}