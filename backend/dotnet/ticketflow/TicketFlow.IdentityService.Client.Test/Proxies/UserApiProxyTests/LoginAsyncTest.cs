using System;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Proxies.UserApiProxyTests
{
    public class LoginAsyncTest : UserApiProxyTestBase<string>
    {
        private const string LoginRequestJson = "{}";

        private readonly LoginRequest loginRequest = new LoginRequest(Email, Password);

        [Fact]
        public async Task LoginAsync_RequestUrl_ShouldSendRequestToRightEndpoint_Async()
        {
            var expectedUri = new Uri($"{ServiceUrl}/users/login");

            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => AreUriEqual(expectedUri, message.RequestUri)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        public async Task LoginAsync_RequestMethod_ShouldSendPostRequest_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Post),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        public async Task LoginAsync_ContentType_ShouldSetContentTypeAsApplicationJson_Async()
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
        public async Task LoginAsync_RequestBody_ShouldSerializeRequestModelInJsonViaSerializer_Async()
        {
            await RunTestAsync(_ =>
            {
                JsonSerializerMock.Received().Serialize(loginRequest);
            });
        }

        [Fact]
        public async Task LoginAsync_RequestBody_ShouldSendRequestWithSerializedModelInBody_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => CheckHttpMessageBody(message, LoginRequestJson)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        public async Task LoginAsync_BodyEncoding_ShouldEncodeBodyInUTF8_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => message.Content.Headers.ContentType.CharSet.Equals(Utf8Text)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        public async Task LoginAsync_Result_ShouldReturnStringResultWithoutConversion_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Any<HttpRequestMessage>(),
                    Arg.Is<Func<string, string>>(convertFunc => convertFunc(Token) == Token));
            });
        }

        [Fact]
        public async Task LoginAsync_Result_ShouldReturnResultFromSender_Async()
        {
            await RunTestAsync(result => Assert.Equal(Token, result));
        }

        protected override async Task RunTestAsync(Func<string, Task> assertActionAsync)
        {
            JsonSerializerMock.Serialize<LoginRequest>(default).ReturnsForAnyArgs(LoginRequestJson);
            IdentityServiceMessageSenderMock.SendAsync(default, default(Func<string, string>)).ReturnsForAnyArgs(Token);

            string result = await UserApiProxy.LoginAsync(loginRequest);

            await assertActionAsync(result);
        }
    }
}