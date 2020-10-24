using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicketFlow.Common.Serializers;
using TicketFlow.Common.ServiceUrl.Providers;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Models;
using TicketFlow.ProfileService.Client.Extensibility.Proxies;
using TicketFlow.ProfileService.Client.Extensibility.Serializers;
using TicketFlow.ProfileService.Client.Senders;

namespace TicketFlow.ProfileService.Client.Proxies
{
    internal class ProfileApiProxy : IProfileApiProxy
    {
        private const string ServiceName = "ProfileService";

        private readonly IProfileSerializer profileSerializer;
        private readonly IProfileServiceMessageSender serviceMessageSender;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IServiceUrlProvider serviceUrlProvider;

        public ProfileApiProxy(IProfileSerializer profileSerializer, IProfileServiceMessageSender serviceMessageSender, IJsonSerializer jsonSerializer, IServiceUrlProvider serviceUrlProvider)
        {
            this.profileSerializer = profileSerializer;
            this.serviceMessageSender = serviceMessageSender;
            this.jsonSerializer = jsonSerializer;
            this.serviceUrlProvider = serviceUrlProvider;
        }

        public async Task<IProfile> GetByIdAsync(int id)
        {
            string requestUrl = $"{GetProfileApiUrlAsync()}/by-id/{id}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            ProfileSerializationModel serializationModel = await serviceMessageSender.SendAsync<ProfileSerializationModel>(httpRequest);

            return profileSerializer.Deserialize(serializationModel);
        }

        public async Task<IProfile> GetByUserEmailAsync(string email)
        {
            string requestUrl = $"{GetProfileApiUrlAsync()}/by-user";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(email);

            ProfileSerializationModel serializationModel = await serviceMessageSender.SendAsync<ProfileSerializationModel>(httpRequest);

            return profileSerializer.Deserialize(serializationModel);
        }

        public async Task<IProfile> AddAsync(ProfileCreationModel profileCreationModel)
        {
            string requestUrl = $"{GetProfileApiUrlAsync()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(profileCreationModel), Encoding.UTF8, "application/json");

            ProfileSerializationModel serializationModel = await serviceMessageSender.SendAsync<ProfileSerializationModel>(httpRequest);

            return profileSerializer.Deserialize(serializationModel);
        }

        private async Task<string> GetProfileApiUrlAsync()
        {
            string ticketServiceUrl = await serviceUrlProvider.GetUrlAsync(ServiceName);
            return $"{ticketServiceUrl}/profiles";
        }
    }
}