using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.Serializers;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Extensibility.Proxies;
using TicketFlow.IdentityService.Client.Extensibility.Serializers;
using TicketFlow.IdentityService.Client.Providers;
using TicketFlow.IdentityService.Client.Senders;

namespace TicketFlow.IdentityService.Client.Proxies
{
    internal class UserApiProxy : IUserApiProxy
    {
        private readonly IUserSerializer serializer;
        private readonly IIdentityServiceMessageSender serviceMessageSender;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IIdentityServiceUrlProvider serviceUrlProvider;

        public UserApiProxy(IUserSerializer serializer, IIdentityServiceMessageSender serviceMessageSender, IJsonSerializer jsonSerializer, IIdentityServiceUrlProvider serviceUrlProvider)
        {
            this.serializer = serializer;
            this.serviceMessageSender = serviceMessageSender;
            this.jsonSerializer = jsonSerializer;
            this.serviceUrlProvider = serviceUrlProvider;
        }

        public async Task<IAuthorizedUser> GetByTokenAsync(string token)
        {
            string requestUrl = await FormServiceUrl("/getByToken");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(token);

            UserSerializationModel serializationModel = await serviceMessageSender.SendAsync<UserSerializationModel>(httpRequest);

            IUser user = serializer.Deserialize(serializationModel);
            return user is IAuthorizedUser authorizedUser ? authorizedUser : throw new NotFoundException($"User with token={token} is not found");
        }

        public async Task<IUser> GetByEmailAsync(string email)
        {
            string requestUrl = await FormServiceUrl("/getByEmail");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(email);

            UserSerializationModel serializationModel = await serviceMessageSender.SendAsync<UserSerializationModel>(httpRequest);

            return serializer.Deserialize(serializationModel);
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
            string requestUrl = await FormServiceUrl("/login");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            return await serviceMessageSender.SendAsync(httpRequest, text => text);
        }

        public async Task<string> RegisterAsync(RegisterRequest registerRequest)
        {
            string requestUrl = await FormServiceUrl("/register");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json");

            return await serviceMessageSender.SendAsync(httpRequest, text => text);
        }

        public async Task<string> LogoutAsync(string token)
        {
            string requestUrl = await FormServiceUrl("/logout");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(token);

            return await serviceMessageSender.SendAsync(httpRequest, text => text);
        }

        private async Task<string> FormServiceUrl(string requestUrl)
        {
            string serviceUrl = await serviceUrlProvider.GetUrlAsync();
            return $"{serviceUrl}/users{requestUrl}";
        }
    }
}