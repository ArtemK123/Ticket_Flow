using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Providers;
using Xunit;

namespace TicketFlow.Common.Test.ServiceUrl.Providers
{
    public class ServiceUrlProvidingTypeProviderTest
    {
        private const string ServiceUrlProvidingTypeSettingPath = "TicketFlow:ServiceUrlProvidingType";

        [Fact]
        public void GetServiceUrlResolvingType_SettingIsNotDefined_ShouldReturnFromDefaultEnumValue()
        {
            RunGetServiceUrlResolvingTypeTest(
                new Dictionary<string, string>(),
                actual => Assert.Equal(default, actual));
        }

        [Fact]
        public void GetServiceUrlResolvingType_SettingIsInvalid_ShouldReturnDefaultEnumValue()
        {
            var settings = new Dictionary<string, string>
            {
                { ServiceUrlProvidingTypeSettingPath, "invalid" }
            };

            RunGetServiceUrlResolvingTypeTest(settings, actual => Assert.Equal(default, actual));
        }

        [Fact]
        public void GetServiceUrlResolvingType_SettingInWrongCase_ShouldReturnDefaultEnumValue()
        {
            var settings = new Dictionary<string, string>
            {
                { ServiceUrlProvidingTypeSettingPath, "fromconsul" }
            };

            RunGetServiceUrlResolvingTypeTest(settings, actual => Assert.Equal(default, actual));
        }

        [Theory]
        [InlineData("FromSetting", ServiceUrlProvidingType.FromSettings)]
        [InlineData("FromConsul", ServiceUrlProvidingType.FromConsul)]
        internal void GetServiceUrlResolvingType_ValidSetting_ShouldReturnCorrectProvidingType(string settingValue, ServiceUrlProvidingType expected)
        {
            var settings = new Dictionary<string, string>
            {
                { ServiceUrlProvidingTypeSettingPath, settingValue }
            };

            RunGetServiceUrlResolvingTypeTest(settings, actual => Assert.Equal(expected, actual));
        }

        private static void RunGetServiceUrlResolvingTypeTest(IReadOnlyDictionary<string, string> settings, Action<ServiceUrlProvidingType> assertAction)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            var serviceUrlProvidingTypeProvider = new ServiceUrlProvidingTypeProvider(configuration);
            ServiceUrlProvidingType actual = serviceUrlProvidingTypeProvider.GetServiceUrlResolvingType();

            assertAction(actual);
        }
    }
}