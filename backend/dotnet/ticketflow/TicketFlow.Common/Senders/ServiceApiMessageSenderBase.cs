using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.Common.Serializers;
using TicketFlow.Common.Validators;

namespace TicketFlow.Common.Senders
{
    public abstract class ServiceApiMessageSenderBase : IServiceApiMessageSender
    {
        private readonly IServiceResponseValidator responseValidator;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IJsonSerializer jsonSerializer;

        protected ServiceApiMessageSenderBase(IServiceResponseValidator responseValidator, IHttpClientFactory httpClientFactory, IJsonSerializer jsonSerializer)
        {
            this.responseValidator = responseValidator;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializer = jsonSerializer;
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage httpRequestMessage)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequestMessage);

            await responseValidator.ValidateAsync(httpResponse);
            string responseBodyJson = await httpResponse.Content.ReadAsStringAsync();

            return jsonSerializer.Deserialize<T>(responseBodyJson);
        }

        public async Task SendAsync(HttpRequestMessage httpRequestMessage)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequestMessage);

            await responseValidator.ValidateAsync(httpResponse);
        }
    }
}