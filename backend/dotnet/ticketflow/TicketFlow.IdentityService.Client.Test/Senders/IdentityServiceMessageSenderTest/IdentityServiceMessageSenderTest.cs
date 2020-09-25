using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NSubstitute;
using TicketFlow.Common.Serializers;
using TicketFlow.IdentityService.Client.Senders;
using TicketFlow.IdentityService.Client.Validators;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Senders.IdentityServiceMessageSenderTest
{
    public class IdentityServiceMessageSenderTest
    {
        private const string ResponseBody = "{ test: test }";
        private const string ExpectedValue = "expected";

        private static readonly string SenderTypeName = typeof(IdentityServiceMessageSender).ToString();

        private readonly HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://test.com");
        private readonly HttpResponseMessage httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(ResponseBody)
        };

        private readonly IIdentityServiceResponseValidator identityServiceResponseValidatorMock;
        private readonly IHttpClientFactory httpClientFactoryMock;
        private readonly IJsonSerializer jsonSerializerMock;

        private readonly IdentityServiceMessageSender identityServiceMessageSender;

        public IdentityServiceMessageSenderTest()
        {
            identityServiceResponseValidatorMock = CreateIdentityServiceResponseValidatorMock();
            httpClientFactoryMock = CreateHttpClientFactoryMock();
            jsonSerializerMock = Substitute.For<IJsonSerializer>();

            identityServiceMessageSender = new IdentityServiceMessageSender(identityServiceResponseValidatorMock, httpClientFactoryMock, jsonSerializerMock);
        }

        [Fact]
        public async Task SendAsync_WithReturn_ShouldCreateHttpClientViaFactory_Async()
        {
            await identityServiceMessageSender.SendAsync(httpRequestMessage, text => text);

            httpClientFactoryMock.Received().CreateClient(SenderTypeName);
        }

        [Fact]
        public async Task SendAsync_WithReturn_ShouldPassHttpResponseToValidator_Async()
        {
            await identityServiceMessageSender.SendAsync(httpRequestMessage, text => text);

            await identityServiceResponseValidatorMock.Received().ValidateAsync(httpResponseMessage);
        }

        [Fact]
        public async Task SendAsync_WithReturn_ConvertFunc_ShouldConvertResponseBodyViaConvertFunc_Async()
        {
            var actual = await identityServiceMessageSender.SendAsync(httpRequestMessage, text => ExpectedValue);

            Assert.Equal(ExpectedValue, actual);
        }

        [Fact]
        public async Task SendAsync_WithReturn_DefaultConvertFunc_ShouldUseJsonSerializerAsDefaultConverter_Async()
        {
            jsonSerializerMock.Deserialize<string>(default).ReturnsForAnyArgs(ExpectedValue);

            var actual = await identityServiceMessageSender.SendAsync<string>(httpRequestMessage);

            Assert.Equal(ExpectedValue, actual);
        }

        [Fact]
        public async Task SendAsync_WithReturn_DefaultConvertFunc_ShouldRunCaseInsensitiveJsonDeserialization_Async()
        {
            jsonSerializerMock.Deserialize<string>(default).ReturnsForAnyArgs(ExpectedValue);

            await identityServiceMessageSender.SendAsync<string>(httpRequestMessage);

            jsonSerializerMock.Received().Deserialize<string>(ResponseBody, Arg.Is<JsonSerializerOptions>(options => options.PropertyNameCaseInsensitive));
        }

        [Fact]
        public async Task SendAsync_WithoutReturn_ShouldCreateHttpClientViaFactory_Async()
        {
            await identityServiceMessageSender.SendAsync(httpRequestMessage);

            httpClientFactoryMock.Received().CreateClient(SenderTypeName);
        }

        [Fact]
        public async Task SendAsync_WithoutReturn_ShouldPassHttpResponseToValidator_Async()
        {
            await identityServiceMessageSender.SendAsync(httpRequestMessage);

            await identityServiceResponseValidatorMock.Received().ValidateAsync(httpResponseMessage);
        }

        private static HttpClient CreateHttpClient(HttpResponseMessage responseMessage)
        {
            HttpMessageHandler httpMessageHandler = new FakeHttpMessageHandler(responseMessage);

            return new HttpClient(httpMessageHandler);
        }

        private IHttpClientFactory CreateHttpClientFactoryMock()
        {
            HttpClient httpClient = CreateHttpClient(httpResponseMessage);

            var mock = Substitute.For<IHttpClientFactory>();
            mock.CreateClient(default).ReturnsForAnyArgs(httpClient);

            return mock;
        }

        private IIdentityServiceResponseValidator CreateIdentityServiceResponseValidatorMock()
        {
            var substitute = Substitute.For<IIdentityServiceResponseValidator>();
            substitute.ValidateAsync(httpResponseMessage).Wait();
            return substitute;
        }
    }
}