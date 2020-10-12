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
    public class GetByTokenAsyncTest : UserApiProxyTestBase<IAuthorizedUser>
    {
        [Fact]
        internal async Task GetByTokenAsync_RequestUrl_ShouldSendRequestToRightEndpoint_Async()
        {
            await RunTestAsync(async _ =>
            {
                Uri expectedUri = new Uri($"{ServiceUrl}/users/getByToken");

                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => AreUriEqual(expectedUri, message.RequestUri)));
            });
        }

        [Fact]
        internal async Task GetByTokenAsync_RequestMethod_ShouldSendPostRequest_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Post));
            });
        }

        [Fact]
        internal async Task GetByTokenAsync_RequestBody_ShouldSendTokenAsRequestBody_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => CheckHttpMessageBody(message, Token)));
            });
        }

        [Fact]
        internal async Task GetByTokenAsync_ContentType_ShouldSetContentTypeAsTextPlain_Async()
        {
            const string expectedContentType = "text/plain";

            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(
                        message => message.Content.Headers.ContentType.MediaType.Equals(
                                expectedContentType,
                                StringComparison.Ordinal)));
            });
        }

        [Fact]
        internal async Task GetByEmailAsync_BodyEncoding_ShouldEncodeRequestBodyInUtf8_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => message.Content.Headers.ContentType.CharSet.Equals(Utf8Text)));
            });
        }

        [Fact]
        internal async Task GetByTokenAsync_SerializationModel_ShouldPassSerializationModelToSerializer_Async()
        {
            await RunTestAsync(_ =>
            {
                UserSerializerMock.Received().Deserialize(UserSerializationModel);
            });
        }

        [Fact]
        internal async Task GetByTokenAsync_NonAuthorizedUser_ShouldThrowNotFoundException_Async()
        {
            IdentityServiceMessageSenderMock.SendAsync<UserSerializationModel>(default).ReturnsForAnyArgs(Task.FromResult(UserSerializationModel));
            UserSerializerMock.Deserialize(default).ReturnsForAnyArgs(UserMock);

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => UserApiProxy.GetByTokenAsync(Token));

            string expectedExceptionMessage = $"User with token={Token} is not found";
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        internal async Task GetByTokenAsync_Result_ShouldReturnAuthorizedUserFromSerializer_Async()
        {
            await RunTestAsync(authorizedUser => Assert.Same(AuthorizedUserMock, authorizedUser));
        }

        protected override async Task RunTestAsync(Func<IAuthorizedUser, Task> assertActionAsync)
        {
            IdentityServiceMessageSenderMock.SendAsync<UserSerializationModel>(default).ReturnsForAnyArgs(Task.FromResult(UserSerializationModel));
            UserSerializerMock.Deserialize(default).ReturnsForAnyArgs(AuthorizedUserMock);

            IAuthorizedUser actual = await UserApiProxy.GetByTokenAsync(Token);

            await assertActionAsync(actual);
        }
    }
}