using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        internal void ProvidingType_FromSettings()
        {
            ServiceUrlProvidingType expected = ServiceUrlProvidingType.FromSettings;
            var actual = CreateServiceUrlFromSettingsProvider(new Dictionary<string, string>()).ProvidingType;
            Assert.Equal(expected, actual);
        }

        [Fact]
        internal async Task GetUrlAsync_SettingIsNotDefined_ShouldThrowException()
        {
            await Assert.ThrowsAsync<Exception>(() => CreateServiceUrlFromSettingsProvider(new Dictionary<string, string>()).GetUrlAsync(ServiceName));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        internal async Task GetUrlAsync_UrlIsNullOrEmpty_ShouldThrowException(string url)
        {
            var settings = new Dictionary<string, string>
            {
                { UrlSettingPath, url }
            };

            await Assert.ThrowsAsync<Exception>(() => CreateServiceUrlFromSettingsProvider(settings).GetUrlAsync(ServiceName));
        }

        [Fact]
        internal async Task GetUrlAsync_ExceptionMessage_ShouldThrowExceptionWithCorrectMessage()
        {
            var expected = $"Please, specify the url for service {ServiceName} in the configuration file by the path {UrlSettingPath}";

            Exception exception = await Assert.ThrowsAsync<Exception>(() => CreateServiceUrlFromSettingsProvider(new Dictionary<string, string>()).GetUrlAsync(ServiceName));

            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        internal async Task GetUrlAsync_ValidUrl_ShouldReturnUrlFromSettings()
        {
            const string expectedUrl = "http://localhost:9000";
            var settings = new Dictionary<string, string>
            {
                { UrlSettingPath, expectedUrl }
            };

            string actualUrl = await CreateServiceUrlFromSettingsProvider(settings).GetUrlAsync(ServiceName);

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