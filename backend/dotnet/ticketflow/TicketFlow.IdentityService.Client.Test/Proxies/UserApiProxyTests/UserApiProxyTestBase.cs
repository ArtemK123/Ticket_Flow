using NSubstitute;
using TicketFlow.Common.Serializers;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Serializers;
using TicketFlow.IdentityService.Client.Providers;
using TicketFlow.IdentityService.Client.Proxies;
using TicketFlow.IdentityService.Client.Senders;

namespace TicketFlow.IdentityService.Client.Test.Proxies.UserApiProxyTests
{
    public abstract class UserApiProxyTestBase
    {
        protected const string ServiceUrl = "http://localhost:9001";
        protected const string Token = "jwt.jwt.jwt";

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

        private protected IIdentityServiceUrlProvider IdentityServiceUrlProviderMock { get; }

        private protected UserApiProxy UserApiProxy { get; }

        private static IIdentityServiceUrlProvider CreateIdentityServiceUrlProviderMock(string url)
        {
            var mock = Substitute.For<IIdentityServiceUrlProvider>();
            mock.GetUrl().Returns(url);
            return mock;
        }
    }
}