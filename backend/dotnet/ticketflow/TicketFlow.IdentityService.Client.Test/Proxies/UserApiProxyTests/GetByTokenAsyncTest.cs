using System;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using TicketFlow.Common.Exceptions;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Proxies.UserApiProxyTests
{
    public class GetByTokenAsyncTest : UserApiProxyTestBase
    {
        [Fact]
        public async Task GetByTokenAsync_RequestUrl_ShouldSendRequestToRightEndpoint_Async()
        {
            await RunGetByTokenAsyncTestAsync(async _ =>
            {
                Uri expectedUri = new Uri($"{ServiceUrl}/users/getByToken");

                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => Uri.Compare(expectedUri, message.RequestUri, UriComponents.AbsoluteUri, UriFormat.UriEscaped, StringComparison.Ordinal) == 0));
            });
        }

        [Fact]
        public async Task GetByTokenAsync_RequestUrl_ShouldGetServiceUrlFromProvider_Async()
        {
            await RunGetByTokenAsyncTestAsync(_ =>
            {
                IdentityServiceUrlProviderMock.Received().GetUrl();
            });
        }

        [Fact]
        public async Task GetByTokenAsync_RequestMethod_ShouldSendPostRequest_Async()
        {
            IdentityServiceMessageSenderMock.SendAsync<UserSerializationModel>(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(Task.FromResult(UserSerializationModel));
            UserSerializerMock.Deserialize(UserSerializationModel).Returns(AuthorizedUserMock);

            await UserApiProxy.GetByTokenAsync(Token);

            await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                Arg.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Post));
        }

        [Fact]
        public async Task GetByTokenAsync_RequestBody_ShouldSendTokenAsRequestBody_Async()
        {
            await RunGetByTokenAsyncTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => CheckHttpMessageBody(message, Token)));
            });
        }

        [Fact]
        public async Task GetByTokenAsync_ContentType_ShouldSetContentTypeAsTextPlain()
        {
            const string expectedContentType = "text/plain";

            await RunGetByTokenAsyncTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(
                        message => message.Content.Headers.ContentType.MediaType.Equals(
                                expectedContentType,
                                StringComparison.Ordinal)));
            });
        }

        [Fact]
        public async Task GetByTokenAsync_SerializationModel_ShouldPassSerializationModelToProvider()
        {
            await RunGetByTokenAsyncTestAsync(_ =>
            {
                UserSerializerMock.Received().Deserialize(UserSerializationModel);
            });
        }

        [Fact]
        public async Task GetByTokenAsync_NonAuthorizedUser_ShouldThrowNotException()
        {
            IdentityServiceMessageSenderMock.SendAsync<UserSerializationModel>(default).ReturnsForAnyArgs(Task.FromResult(UserSerializationModel));
            UserSerializerMock.Deserialize(default).ReturnsForAnyArgs(UserMock);

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => UserApiProxy.GetByTokenAsync(Token));

            string expectedExceptionMessage = $"User with token={Token} is not found";
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        private async Task RunGetByTokenAsyncTestAsync(Action<IAuthorizedUser> assertAction)
        {
            Task AssertActionAsync(IAuthorizedUser user) => Task.Run(() => assertAction(user));

            await RunGetByTokenAsyncTestAsync(AssertActionAsync);
        }

        private async Task RunGetByTokenAsyncTestAsync(Func<IAuthorizedUser, Task> assertActionAsync)
        {
            IdentityServiceMessageSenderMock.SendAsync<UserSerializationModel>(default).ReturnsForAnyArgs(Task.FromResult(UserSerializationModel));
            UserSerializerMock.Deserialize(default).ReturnsForAnyArgs(AuthorizedUserMock);

            await UserApiProxy.GetByTokenAsync(Token);

            await assertActionAsync(AuthorizedUserMock);
        }

        private bool CheckHttpMessageBody(HttpRequestMessage message, string expectedBody)
        {
            Task<string> getBodyTask = message.Content.ReadAsStringAsync();
            getBodyTask.Wait();
            string actualBody = getBodyTask.Result;

            return expectedBody.Equals(actualBody, StringComparison.Ordinal);
        }
    }
}