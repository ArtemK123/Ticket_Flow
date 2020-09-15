using TicketFlow.Common.Providers;
using Xunit;

namespace TicketFlow.Common.Test.Providers.UrlFromConfigProviderTest
{
    public class PortSettingValidationTest
    {
        private const string ServiceBaseUrl = "http://localhost";
        private const int MinimumPortValue = 1024;
        private const int MaximumPortValue = 64000;

        private readonly UrlFromConfigProvider urlFromConfigProvider;

        public PortSettingValidationTest()
        {
            urlFromConfigProvider = new UrlFromConfigProvider();
        }

        [Fact]
        public void GetUrl_GetPortSetting_ShouldUseGivenPortWhenValid()
        {

        }
    }
}