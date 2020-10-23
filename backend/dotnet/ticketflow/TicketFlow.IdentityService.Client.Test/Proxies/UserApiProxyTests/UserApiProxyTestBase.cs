using System;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using TicketFlow.Common.Serializers;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Serializers;
using TicketFlow.IdentityService.Client.Providers;
using TicketFlow.IdentityService.Client.Proxies;
using TicketFlow.IdentityService.Client.Senders;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Proxies.UserApiProxyTests
{
    public abstract class UserApiProxyTestBase<TResult>
    {
        protected const string ServiceUrl = "http://localhost:9001";
        protected const string Token = "jwt.jwt.jwt";
        protected const string Email = "test@gmail.com";
        protected const string Password = "top secret!";
        protected const string Utf8Text = "utf-8";

        protected UserApiProxyTestBase()
        {
            UserSerializerMock = Substitute.For<IUserSerializer>();
            IdentityServiceMessageSenderMock = Substitute.For<IIdentityServiceMessageSender>();
            JsonSerializerMock = Substitute.For<IJsonSerializer>();
            IdentityServiceUrlProviderMock = CreateIdentityServiceUrlProviderMock(ServiceUrl);

            UserApiProxy = new UserApiProxy(UserSerializerMock, IdentityServiceMessageSenderMock, JsonSerializerMock, IdentityServiceUrlProviderMock);
        }

        protected UserSerializationModel UserSerializationModel { get; } = new UserSerializationModel();

        protected IUser UserMock { get; } = Substitute.For<IUser>();

        protected IAuthorizedUser AuthorizedUserMock { get; } = Substitute.For<IAuthorizedUser>();

        protected IUserSerializer UserSerializerMock { get; }

        protected IIdentityServiceMessageSender IdentityServiceMessageSenderMock { get; }

        protected IJsonSerializer JsonSerializerMock { get; }

        private protected UserApiProxy UserApiProxy { get; }

        private IIdentityServiceUrlProvider IdentityServiceUrlProviderMock { get; }

        [Fact]
        public async Task Common_RequestUrl_ShouldGetServiceUrlFromProvider_Async()
        {
            await RunTestAsync(_ =>
            {
                IdentityServiceUrlProviderMock.Received().GetUrlAsync();
            });
        }

        protected static bool AreUriEqual(Uri a, Uri b) => Uri.Compare(a, b, UriComponents.AbsoluteUri, UriFormat.UriEscaped, StringComparison.Ordinal) == 0;

        protected async Task RunTestAsync(Action<TResult> assertAction)
        {
            Task AssertActionAsync(TResult actual) => Task.Run(() => assertAction(actual));

            await RunTestAsync(AssertActionAsync);
        }

        protected abstract Task RunTestAsync(Func<TResult, Task> assertActionAsync);

        protected bool CheckHttpMessageBody(HttpRequestMessage message, string expectedBody)
        {
            Task<string> getBodyTask = message.Content.ReadAsStringAsync();
            getBodyTask.Wait();
            string actualBody = getBodyTask.Result;

            return expectedBody.Equals(actualBody, StringComparison.Ordinal);
        }

        private static IIdentityServiceUrlProvider CreateIdentityServiceUrlProviderMock(string url)
        {
            var mock = Substitute.For<IIdentityServiceUrlProvider>();
            mock.GetUrlAsync().Returns(Task.FromResult(url));
            return mock;
        }
    }
}