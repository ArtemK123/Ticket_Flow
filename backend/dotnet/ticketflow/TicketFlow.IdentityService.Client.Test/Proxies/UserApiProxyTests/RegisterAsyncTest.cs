using System;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Proxies.UserApiProxyTests
{
    public class RegisterAsyncTest : UserApiProxyTestBase<string>
    {
        private const string SerializedRequest = "{}";
        private const string Result = "Registered successfully with id=1";

        private readonly RegisterRequest registerRequest = new RegisterRequest(Email, Password);

        [Fact]
        public async Task RegisterAsync_RequestUrl_ShouldSendRequestToRightEndpoint_Async()
        {
            var expectedUri = new Uri($"{ServiceUrl}/users/register");

            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => AreUriEqual(expectedUri, message.RequestUri)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        public async Task RegisterAsync_RequestMethod_ShouldSendPostRequest_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Post),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        public async Task RegisterAsync_ContentType_ShouldSetContentTypeAsApplicationJson_Async()
        {
            const string expectedContentType = "application/json";

            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(
                        message => message.Content.Headers.ContentType.MediaType.Equals(
                            expectedContentType,
                            StringComparison.Ordinal)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        public async Task RegisterAsync_RequestBody_ShouldSerializeRequestModelInJsonViaSerializer_Async()
        {
            await RunTestAsync(_ =>
            {
                JsonSerializerMock.Received().Serialize(registerRequest);
            });
        }

        [Fact]
        public async Task RegisterAsync_RequestBody_ShouldSendRequestWithSerializedModelInBody_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => CheckHttpMessageBody(message, SerializedRequest)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        public async Task RegisterAsync_BodyEncoding_ShouldEncodeBodyInUTF8_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => message.Content.Headers.ContentType.CharSet.Equals(Utf8Text)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        public async Task RegisterAsync_Result_ShouldReturnStringResultWithoutConversion_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Any<HttpRequestMessage>(),
                    Arg.Is<Func<string, string>>(convertFunc => convertFunc(Result) == Result));
            });
        }

        [Fact]
        public async Task RegisterAsync_Result_ShouldReturnResultFromSender_Async()
        {
            await RunTestAsync(result => Assert.Equal(Result, result));
        }

        protected override async Task RunTestAsync(Func<string, Task> assertActionAsync)
        {
            JsonSerializerMock.Serialize<RegisterRequest>(default).ReturnsForAnyArgs(SerializedRequest);
            IdentityServiceMessageSenderMock.SendAsync(default, default(Func<string, string>)).ReturnsForAnyArgs(Result);

            string actual = await UserApiProxy.RegisterAsync(registerRequest);

            await assertActionAsync(actual);
        }
    }
}