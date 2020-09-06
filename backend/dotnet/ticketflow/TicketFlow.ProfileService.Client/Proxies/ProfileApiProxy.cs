using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.Serializers;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Models;
using TicketFlow.ProfileService.Client.Extensibility.Proxies;
using TicketFlow.ProfileService.Client.Extensibility.Serializers;
using TicketFlow.ProfileService.Client.Senders;

namespace TicketFlow.ProfileService.Client.Proxies
{
    internal class ProfileApiProxy : IProfileApiProxy
    {
        private readonly IConfiguration configuration;
        private readonly IProfileSerializer profileSerializer;
        private readonly IProfileServiceMessageSender serviceMessageSender;
        private readonly IJsonSerializer jsonSerializer;

        public ProfileApiProxy(IConfiguration configuration, IProfileSerializer profileSerializer, IProfileServiceMessageSender serviceMessageSender, IJsonSerializer jsonSerializer)
        {
            this.configuration = configuration;
            this.profileSerializer = profileSerializer;
            this.serviceMessageSender = serviceMessageSender;
            this.jsonSerializer = jsonSerializer;
        }

        public async Task<IProfile> GetByIdAsync(int id)
        {
            string requestUrl = $"{GetProfileApiUrl()}/by-id/{id}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            ProfileSerializationModel serializationModel = await serviceMessageSender.SendAsync<ProfileSerializationModel>(httpRequest);

            return profileSerializer.Deserialize(serializationModel);
        }

        public async Task<IProfile> GetByUserEmailAsync(string email)
        {
            string requestUrl = $"{GetProfileApiUrl()}/by-user";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(email);

            ProfileSerializationModel serializationModel = await serviceMessageSender.SendAsync<ProfileSerializationModel>(httpRequest);

            return profileSerializer.Deserialize(serializationModel);
        }

        public async Task<IProfile> AddAsync(ProfileCreationModel profileCreationModel)
        {
            string requestUrl = $"{GetProfileApiUrl()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(profileCreationModel), Encoding.UTF8, "application/json");

            ProfileSerializationModel serializationModel = await serviceMessageSender.SendAsync<ProfileSerializationModel>(httpRequest);

            return profileSerializer.Deserialize(serializationModel);
        }

        private string GetProfileApiUrl()
        {
            string ticketServiceUrl = configuration.GetValue<string>("TicketFlow:ProfileService:Url");
            return $"{ticketServiceUrl}/profiles";
        }
    }
}