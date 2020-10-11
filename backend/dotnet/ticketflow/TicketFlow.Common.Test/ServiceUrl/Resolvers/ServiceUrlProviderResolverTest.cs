using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TicketFlow.Common.Extensions;
using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Providers;
using TicketFlow.Common.ServiceUrl.Resolvers;
using Xunit;

namespace TicketFlow.Common.Test.ServiceUrl.Resolvers
{
    public class ServiceUrlProviderResolverTest
    {
        public static IEnumerable<object[]> ServiceUrlProvidingTypeNamesTestData
            => EnumExtensions.GetAllValues<ServiceUrlProvidingType>().Select(providingType => new object[] { providingType }).ToArray();

        [Fact]
        public void Resolve_ProviderIsNotFound_ShouldThrowException()
        {
            IEnumerable<IServiceUrlProvider> providers = Array.Empty<IServiceUrlProvider>();
            Assert.Throws<InvalidOperationException>(() => CreateServiceUrlProviderResolver(providers).Resolve(ServiceUrlProvidingType.FromSettings));
        }

        [Fact]
        public void Resolve_MultipleProviders_ShouldThrowException()
        {
            IEnumerable<IServiceUrlProvider> providers = new[]
            {
                CreateServiceUrlProviderMock(ServiceUrlProvidingType.FromSettings),
                CreateServiceUrlProviderMock(ServiceUrlProvidingType.FromSettings)
            };

            Assert.Throws<InvalidOperationException>(() => CreateServiceUrlProviderResolver(providers).Resolve(ServiceUrlProvidingType.FromSettings));
        }

        [Theory]
        [MemberData(nameof(ServiceUrlProvidingTypeNamesTestData))]
        public void Resolve_SingleProvider_ShouldReturnCorrectProvider(ServiceUrlProvidingType providingType)
        {
            IEnumerable<ServiceUrlProvidingType> enumValues = EnumExtensions.GetAllValues<ServiceUrlProvidingType>();
            IEnumerable<IServiceUrlProvider> providers = enumValues.Select(CreateServiceUrlProviderMock);

            IServiceUrlProvider actual = CreateServiceUrlProviderResolver(providers).Resolve(providingType);

            Assert.Equal(providingType, actual.ProvidingType);
        }

        private static ServiceUrlProviderResolver CreateServiceUrlProviderResolver(IEnumerable<IServiceUrlProvider> providers)
        {
            return new ServiceUrlProviderResolver(providers);
        }

        private IServiceUrlProvider CreateServiceUrlProviderMock(ServiceUrlProvidingType serviceUrlProvidingType)
        {
            var substitute = Substitute.For<IServiceUrlProvider>();
            substitute.ProvidingType.Returns(serviceUrlProvidingType);
            return substitute;
        }
    }
}