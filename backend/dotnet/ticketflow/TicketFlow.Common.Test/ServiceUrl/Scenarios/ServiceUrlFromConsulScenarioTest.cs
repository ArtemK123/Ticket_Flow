using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.Providers;
using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Scenarios;
using Xunit;

namespace TicketFlow.Common.Test.ServiceUrl.Scenarios
{
    public class ServiceUrlFromConsulScenarioTest
    {
        private const string ServiceNameArg = "Ticket";
        private const string ConsulServiceName = "ConsulMovie";
        private const string Address = "http://localhost";
        private const string AnotherAddress = "http://test";
        private const int Port = 9004;
        private const int IndexOfRandomlyTakenService = 1;

        private static readonly string ConsulServiceNameSettingPath = $"TicketFlow:{ServiceNameArg}:ConsulName";
        private static readonly string ExpectedExceptionMessage = $"Service with name={ServiceNameArg} is not found";
        private static readonly IReadOnlyDictionary<string, string> DefaultSettings = new Dictionary<string, string>
        {
            { ConsulServiceNameSettingPath, ConsulServiceName }
        };

        private static readonly IReadOnlyCollection<AgentService> AgentServices = new[]
        {
            new AgentService { Service = "InvalidName" },
            new AgentService { Service = ConsulServiceName },
            new AgentService { Service = ConsulServiceName, Address = Address, Port = Port }
        };

        private readonly IAgentEndpoint agentEndpointMock;
        private readonly IRandomValueProvider randomValueProviderMock;

        public ServiceUrlFromConsulScenarioTest()
        {
            agentEndpointMock = Substitute.For<IAgentEndpoint>();
            randomValueProviderMock = CreateRandomValueProviderSubstitute(IndexOfRandomlyTakenService);
        }

        [Fact]
        internal void ProvidingType_ShouldReturnFromConsul()
        {
            ServiceUrlProvidingType actual = CreateServiceUrlFromConsulScenario(DefaultSettings).ProvidingType;
            Assert.Equal(ServiceUrlProvidingType.FromConsul, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        internal async Task GetAsync_ConsulServiceNameFromConfigs_Invalid_ShouldUseServiceNameFromArgs(string invalidServiceName)
        {
            randomValueProviderMock.GetRandomInt(0, 1).Returns(0);
            AgentService agentService = new AgentService { Service = ServiceNameArg, Address = AnotherAddress, Port = Port };
            var settings = new Dictionary<string, string> { { ConsulServiceNameSettingPath, invalidServiceName } };

            await RunGetAsyncTestAsync(settings, CreateQueryResult(new[] { agentService }), url => Assert.Equal($"{AnotherAddress}:{Port}", url));
        }

        [Fact]
        internal async Task GetAsync_Services_ShouldGetServicesFromConsulAgent()
        {
            await RunGetAsyncTestAsync(DefaultSettings, CreateQueryResult(AgentServices), _ => agentEndpointMock.Received().Services());
        }

        [Fact]
        internal async Task GetAsync_Services_NullResponse_ShouldThrowServiceNotFoundException()
        {
            await RunGetAsyncThrowExceptionTestAsync<ServiceNotFoundException>(DefaultSettings, null, ExpectedExceptionMessage);
        }

        [Fact]
        internal async Task GetAsync_SuitableServices_NoServices_ShouldThrowServiceNotFoundException()
        {
            await RunGetAsyncThrowExceptionTestAsync<ServiceNotFoundException>(DefaultSettings, CreateQueryResult(Array.Empty<AgentService>()), ExpectedExceptionMessage);
        }

        [Fact]
        internal async Task GetAsync_SuitableServices_Random_ShouldGetRandomIndexFromRandomProvider()
        {
            await RunGetAsyncTestAsync(DefaultSettings, CreateQueryResult(AgentServices), _ => randomValueProviderMock.Received().GetRandomInt(0, 2));
        }

        [Fact]
        internal async Task GetAsync_ShouldReturnValidUrl()
        {
            await RunGetAsyncTestAsync(DefaultSettings, CreateQueryResult(AgentServices), url => Assert.Equal($"{Address}:{Port}", url));
        }

        private static IConfiguration CreateConfiguration(IReadOnlyDictionary<string, string> settings)
            => new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

        private static IConsulClient CreateConsulClientSubstitute(IAgentEndpoint agent)
        {
            var substitute = Substitute.For<IConsulClient>();
            substitute.Agent.Returns(agent);
            return substitute;
        }

        private static IRandomValueProvider CreateRandomValueProviderSubstitute(int randomValue)
        {
            var substitute = Substitute.For<IRandomValueProvider>();
            substitute.GetRandomInt(default, default).ReturnsForAnyArgs(randomValue);
            return substitute;
        }

        private static QueryResult<Dictionary<string, AgentService>> CreateQueryResult(IReadOnlyCollection<AgentService> agentServices)
            => new QueryResult<Dictionary<string, AgentService>> { Response = agentServices.ToDictionary(_ => Guid.NewGuid().ToString(), agentService => agentService) };

        private ServiceUrlFromConsulScenario CreateServiceUrlFromConsulScenario(IReadOnlyDictionary<string, string> settings)
            => new ServiceUrlFromConsulScenario(CreateConfiguration(settings), CreateConsulClientSubstitute(agentEndpointMock), randomValueProviderMock);

        private async Task RunGetAsyncTestAsync(
            IReadOnlyDictionary<string, string> settings,
            QueryResult<Dictionary<string, AgentService>> allServicesQueryResult,
            Action<string> assertAction)
        {
            agentEndpointMock.Services().ReturnsForAnyArgs(Task.FromResult(allServicesQueryResult));

            string actual = await CreateServiceUrlFromConsulScenario(settings).GetUrlAsync(ServiceNameArg);

            assertAction(actual);
        }

        private async Task RunGetAsyncThrowExceptionTestAsync<TException>(
            IReadOnlyDictionary<string, string> settings,
            QueryResult<Dictionary<string, AgentService>> allServicesQueryResult,
            string expectedExceptionMessage)
            where TException : Exception
        {
            agentEndpointMock.Services().ReturnsForAnyArgs(Task.FromResult(allServicesQueryResult));

            TException exception = await Assert.ThrowsAsync<TException>(() => CreateServiceUrlFromConsulScenario(settings).GetUrlAsync(ServiceNameArg));
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}