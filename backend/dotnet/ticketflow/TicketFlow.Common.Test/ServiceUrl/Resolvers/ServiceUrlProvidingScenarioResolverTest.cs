using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TicketFlow.Common.Extensions;
using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Resolvers;
using TicketFlow.Common.ServiceUrl.Scenarios;
using Xunit;

namespace TicketFlow.Common.Test.ServiceUrl.Resolvers
{
    public class ServiceUrlProvidingScenarioResolverTest
    {
        public static IEnumerable<object[]> ServiceUrlProvidingTypeNamesTestData
            => EnumExtensions.GetAllValues<ServiceUrlProvidingType>().Select(providingType => new object[] { providingType }).ToArray();

        [Fact]
        public void Resolve_ProviderIsNotFound_ShouldThrowException()
        {
            IEnumerable<IServiceUrlProvidingScenario> scenarios = Array.Empty<IServiceUrlProvidingScenario>();
            Assert.Throws<InvalidOperationException>(() => CreateServiceUrlProviderResolver(scenarios).Resolve(ServiceUrlProvidingType.FromSettings));
        }

        [Fact]
        public void Resolve_MultipleProviders_ShouldThrowException()
        {
            IEnumerable<IServiceUrlProvidingScenario> scenarios = new[]
            {
                CreateServiceUrlProvidingScenarioMock(ServiceUrlProvidingType.FromSettings),
                CreateServiceUrlProvidingScenarioMock(ServiceUrlProvidingType.FromSettings)
            };

            Assert.Throws<InvalidOperationException>(() => CreateServiceUrlProviderResolver(scenarios).Resolve(ServiceUrlProvidingType.FromSettings));
        }

        [Theory]
        [MemberData(nameof(ServiceUrlProvidingTypeNamesTestData))]
        public void Resolve_SingleProvider_ShouldReturnCorrectProvider(ServiceUrlProvidingType providingType)
        {
            IEnumerable<ServiceUrlProvidingType> enumValues = EnumExtensions.GetAllValues<ServiceUrlProvidingType>();
            IEnumerable<IServiceUrlProvidingScenario> scenarios = enumValues.Select(CreateServiceUrlProvidingScenarioMock);

            IServiceUrlProvidingScenario actual = CreateServiceUrlProviderResolver(scenarios).Resolve(providingType);

            Assert.Equal(providingType, actual.ProvidingType);
        }

        private static ServiceUrlProvidingScenarioResolver CreateServiceUrlProviderResolver(IEnumerable<IServiceUrlProvidingScenario> providers)
        {
            return new ServiceUrlProvidingScenarioResolver(providers);
        }

        private static IServiceUrlProvidingScenario CreateServiceUrlProvidingScenarioMock(ServiceUrlProvidingType serviceUrlProvidingType)
        {
            var substitute = Substitute.For<IServiceUrlProvidingScenario>();
            substitute.ProvidingType.Returns(serviceUrlProvidingType);
            return substitute;
        }
    }
}