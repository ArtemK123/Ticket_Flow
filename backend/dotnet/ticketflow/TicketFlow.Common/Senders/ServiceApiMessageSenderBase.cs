using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TicketFlow.Common.Serializers;
using TicketFlow.Common.Validators;

namespace TicketFlow.Common.Senders
{
    public abstract class ServiceApiMessageSenderBase : IServiceApiMessageSender
    {
        private readonly IServiceResponseValidator responseValidator;
        private readonly Lazy<IHttpClientFactory> httpClientFactoryLazy;
        private readonly IJsonSerializer jsonSerializer;

        private readonly string httpClientName;

        protected ServiceApiMessageSenderBase(IServiceResponseValidator responseValidator, Lazy<IHttpClientFactory> httpClientFactoryLazy, IJsonSerializer jsonSerializer)
        {
            this.responseValidator = responseValidator;
            this.httpClientFactoryLazy = httpClientFactoryLazy;
            this.jsonSerializer = jsonSerializer;

            httpClientName = GetType().ToString();
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage httpRequestMessage, Func<string, T> convertFunc = null)
        {
            HttpClient httpClient = httpClientFactoryLazy.Value.CreateClient(httpClientName);
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequestMessage);

            await responseValidator.ValidateAsync(httpResponse);
            string responseBodyJson = await httpResponse.Content.ReadAsStringAsync();

            return convertFunc != null ?
                convertFunc(responseBodyJson) :
                jsonSerializer.Deserialize<T>(responseBodyJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task SendAsync(HttpRequestMessage httpRequestMessage)
        {
            HttpClient httpClient = httpClientFactoryLazy.Value.CreateClient(httpClientName);
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequestMessage);

            await responseValidator.ValidateAsync(httpResponse);
        }
    }
}