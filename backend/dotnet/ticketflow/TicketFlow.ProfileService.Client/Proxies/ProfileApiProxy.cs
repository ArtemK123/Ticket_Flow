using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.Serializers;
using TicketFlow.ProfileService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Exceptions;
using TicketFlow.ProfileService.Client.Extensibility.Models;
using TicketFlow.ProfileService.Client.Extensibility.Proxies;
using TicketFlow.ProfileService.Client.Extensibility.Serializers;

namespace TicketFlow.ProfileService.Client.Proxies
{
    internal class ProfileApiProxy : IProfileApiProxy
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IProfileSerializer profileSerializer;

        public ProfileApiProxy(IConfiguration configuration, IHttpClientFactory httpClientFactory, IJsonSerializer jsonSerializer, IProfileSerializer profileSerializer)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializer = jsonSerializer;
            this.profileSerializer = profileSerializer;
        }

        public async Task<IProfile> GetByIdAsync(int id)
        {
            string requestUrl = $"{GetProfileApiUrl()}/by-id/{id}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            await ThrowExceptionOnErrorAsync(httpResponse);
            string responseBodyJson = await httpResponse.Content.ReadAsStringAsync();

            ProfileSerializationModel serializationModel = jsonSerializer.Deserialize<ProfileSerializationModel>(responseBodyJson);
            return profileSerializer.Deserialize(serializationModel);
        }

        public async Task<IProfile> GetByUserEmailAsync(string email)
        {
            string requestUrl = $"{GetProfileApiUrl()}/by-user";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            await ThrowExceptionOnErrorAsync(httpResponse);
            string responseBodyJson = await httpResponse.Content.ReadAsStringAsync();

            ProfileSerializationModel serializationModel = jsonSerializer.Deserialize<ProfileSerializationModel>(responseBodyJson);
            return profileSerializer.Deserialize(serializationModel);
        }

        public async Task<IProfile> AddAsync(ProfileCreationModel profileCreationModel)
        {
            string requestUrl = $"{GetProfileApiUrl()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(profileCreationModel));

            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            await ThrowExceptionOnErrorAsync(httpResponse);

            string responseBodyJson = await httpResponse.Content.ReadAsStringAsync();
            ProfileSerializationModel serializationModel = jsonSerializer.Deserialize<ProfileSerializationModel>(responseBodyJson);

            return profileSerializer.Deserialize(serializationModel);
        }

        private static async Task ThrowExceptionOnErrorAsync(HttpResponseMessage httpResponse)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.NotFound: throw new NotFoundException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: throw new NotUniqueEntityException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: throw new InternalServiceException("Internal error in Profile Service");
            }
        }

        private string GetProfileApiUrl()
        {
            string ticketServiceUrl = configuration.GetValue<string>("TicketFlow:ProfileService:Url");
            return $"{ticketServiceUrl}/profiles";
        }
    }
}