using System;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Proxies.UserApiProxyTests
{
    public class LogoutAsyncTest : UserApiProxyTestBase<string>
    {
        private const string Result = "Logout sucessfully!";

        [Fact]
        internal async Task LogoutAsync_RequestUrl_ShouldSendRequestToRightEndpoint_Async()
        {
            var expectedUri = new Uri($"{ServiceUrl}/users/logout");

            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => AreUriEqual(expectedUri, message.RequestUri)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        internal async Task LogoutAsync_RequestMethod_ShouldSendPostRequest_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Post),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        internal async Task LogoutAsync_ContentType_ShouldSetContentTypeAsTextPlain_Async()
        {
            const string expectedContentType = "text/plain";

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
        internal async Task LogoutAsync_RequestBody_ShouldSendRequestWithTokenInBody_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => CheckHttpMessageBody(message, Token)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        internal async Task LogoutAsync_BodyEncoding_ShouldEncodeBodyInUTF8_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Is<HttpRequestMessage>(message => message.Content.Headers.ContentType.CharSet.Equals(Utf8Text)),
                    Arg.Any<Func<string, string>>());
            });
        }

        [Fact]
        internal async Task LogoutAsync_Result_ShouldReturnStringResultWithoutConversion_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync(
                    Arg.Any<HttpRequestMessage>(),
                    Arg.Is<Func<string, string>>(convertFunc => convertFunc(Result) == Result));
            });
        }

        [Fact]
        internal async Task LogoutAsync_Result_ShouldReturnResultFromSender_Async()
        {
            await RunTestAsync(result => Assert.Equal(Result, result));
        }

        protected override async Task RunTestAsync(Func<string, Task> assertActionAsync)
        {
            IdentityServiceMessageSenderMock.SendAsync(default, default(Func<string, string>)).ReturnsForAnyArgs(Result);

            string actual = await UserApiProxy.LogoutAsync(Token);

            await assertActionAsync(actual);
        }
    }
}