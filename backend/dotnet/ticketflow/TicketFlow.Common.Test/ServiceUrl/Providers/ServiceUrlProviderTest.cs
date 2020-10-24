using System.Threading.Tasks;
using NSubstitute;
using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Providers;
using TicketFlow.Common.ServiceUrl.Resolvers;
using TicketFlow.Common.ServiceUrl.Scenarios;
using Xunit;

namespace TicketFlow.Common.Test.ServiceUrl.Providers
{
    public class ServiceUrlProviderTest
    {
        private const ServiceUrlProvidingType ProvidingType = ServiceUrlProvidingType.FromConsul;
        private const string ServiceName = "Indentity";
        private const string ServiceUrl = "http://localhost:9001";

        private readonly IServiceUrlProvidingScenario serviceUrlProvidingScenarioMock;
        private readonly IServiceUrlProvidingScenarioResolver serviceUrlProvidingScenarioResolverMock;

        private readonly ServiceUrlProvider serviceUrlProvider;

        public ServiceUrlProviderTest()
        {
            IServiceUrlProvidingTypeProvider serviceUrlProvidingTypeProviderMock = CreateServiceUrlProvidingTypeProviderMock(ProvidingType);
            serviceUrlProvidingScenarioMock = CreateServiceUrlProvidingScenarioMock(ServiceUrl);
            serviceUrlProvidingScenarioResolverMock = CreateServiceUrlProvidingScenarioResolverMock(serviceUrlProvidingScenarioMock);

            serviceUrlProvider = new ServiceUrlProvider(serviceUrlProvidingTypeProviderMock, serviceUrlProvidingScenarioResolverMock);
        }

        [Fact]
        internal async Task GetUrlAsync_ProvidingType_ShouldGetProvidingTypeFromProvider()
        {
            await serviceUrlProvider.GetUrlAsync(ServiceName);

            serviceUrlProvidingScenarioResolverMock.Received().Resolve(ProvidingType);
        }

        [Fact]
        internal async Task GetUrlAsync_Scenario_ShouldCallScenarioWithGivenServiceName()
        {
            await serviceUrlProvider.GetUrlAsync(ServiceName);

            await serviceUrlProvidingScenarioMock.Received().GetUrlAsync(ServiceName);
        }

        [Fact]
        internal async Task GetUrlAsync_ReturnedUrl_ShouldReturnUrlFromScenario()
        {
            string actualUrl = await serviceUrlProvider.GetUrlAsync(ServiceName);

            Assert.Equal(ServiceUrl, actualUrl);
        }

        private static IServiceUrlProvidingTypeProvider CreateServiceUrlProvidingTypeProviderMock(ServiceUrlProvidingType providingType)
        {
            var substitute = Substitute.For<IServiceUrlProvidingTypeProvider>();
            substitute.GetServiceUrlResolvingType().ReturnsForAnyArgs(providingType);
            return substitute;
        }

        private static IServiceUrlProvidingScenario CreateServiceUrlProvidingScenarioMock(string url)
        {
            var substitute = Substitute.For<IServiceUrlProvidingScenario>();
            substitute.GetUrlAsync(default).ReturnsForAnyArgs(url);
            return substitute;
        }

        private static IServiceUrlProvidingScenarioResolver CreateServiceUrlProvidingScenarioResolverMock(IServiceUrlProvidingScenario scenario)
        {
            var substitute = Substitute.For<IServiceUrlProvidingScenarioResolver>();
            substitute.Resolve(default).ReturnsForAnyArgs(scenario);
            return substitute;
        }
    }
}