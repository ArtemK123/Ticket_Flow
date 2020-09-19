using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TicketFlow.IdentityService.Client.Providers;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Providers
{
    public class IdentityServiceUrlProviderTest
    {
        private const string UrlSettingValue = "http://localhost:9001";

        private readonly IReadOnlyDictionary<string, string> settings = new Dictionary<string, string>
        {
            { "TicketFlow:IdentityService:Url", UrlSettingValue }
        };

        private readonly IdentityServiceUrlProvider identityServiceUrlProvider;

        public IdentityServiceUrlProviderTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            identityServiceUrlProvider = new IdentityServiceUrlProvider(configuration);
        }

        [Fact]
        public void GetUrl_ShouldGetUrlFromConfiguration()
        {
            var actualUrl = identityServiceUrlProvider.GetUrl();

            Assert.Equal(UrlSettingValue, actualUrl);
        }
    }
}