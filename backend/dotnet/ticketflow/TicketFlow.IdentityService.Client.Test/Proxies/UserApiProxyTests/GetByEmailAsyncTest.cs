using System;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Proxies.UserApiProxyTests
{
    public class GetByEmailAsyncTest : UserApiProxyTestBase<IUser>
    {
        [Fact]
        public async Task GetByEmailAsync_RequestUrl_ShouldSendRequestToRightEndpoint_Async()
        {
            await RunTestAsync(async _ =>
            {
                Uri expectedUri = new Uri($"{ServiceUrl}/users/getByEmail");

                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => AreUriEqual(expectedUri, message.RequestUri)));
            });
        }

        [Fact]
        public async Task GetByEmailAsync_RequestMethod_ShouldSendPostRequest_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Post));
            });
        }

        [Fact]
        public async Task GetByEmailAsync_RequestBody_ShouldSendUserEmailAsRequestBody_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => CheckHttpMessageBody(message, Email)));
            });
        }

        [Fact]
        public async Task GetByEmailAsync_ContentType_ShouldSetContentTypeAsTextPlain_Async()
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
        public async Task GetByEmailAsync_BodyEncoding_ShouldEncodeRequestBodyInUtf8_Async()
        {
            await RunTestAsync(async _ =>
            {
                await IdentityServiceMessageSenderMock.Received().SendAsync<UserSerializationModel>(
                    Arg.Is<HttpRequestMessage>(message => message.Content.Headers.ContentType.CharSet.Equals(Utf8Text)));
            });
        }

        [Fact]
        public async Task GetByEmailAsync_SerializationModel_ShouldPassSerializationModelToSerializer_Async()
        {
            await RunTestAsync(_ =>
            {
                UserSerializerMock.Received().Deserialize(UserSerializationModel);
            });
        }

        [Fact]
        public async Task GetByTokenAsync_Result_ShouldReturnUserFromSerializer_Async()
        {
            await RunTestAsync(user => Assert.Same(UserMock, user));
        }

        protected override async Task RunTestAsync(Func<IUser, Task> assertActionAsync)
        {
            IdentityServiceMessageSenderMock.SendAsync<UserSerializationModel>(default).ReturnsForAnyArgs(Task.FromResult(UserSerializationModel));
            UserSerializerMock.Deserialize(default).ReturnsForAnyArgs(UserMock);

            IUser actual = await UserApiProxy.GetByEmailAsync(Email);

            await assertActionAsync(actual);
        }
    }
}