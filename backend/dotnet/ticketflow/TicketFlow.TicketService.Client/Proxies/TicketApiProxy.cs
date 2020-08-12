using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.Serializers;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Exceptions;
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Proxies;
using TicketFlow.TicketService.Client.Extensibility.Serializers;

namespace TicketFlow.TicketService.Client.Proxies
{
    internal class TicketApiProxy : ITicketApiProxy
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ITicketSerializer ticketSerializer;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IConfiguration configuration;

        public TicketApiProxy(IHttpClientFactory httpClientFactory, IConfiguration configuration, ITicketSerializer ticketSerializer, IJsonSerializer jsonSerializer)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.ticketSerializer = ticketSerializer;
            this.jsonSerializer = jsonSerializer;
        }

        public async Task<IReadOnlyCollection<ITicket>> GetByMovieIdAsync(int movieId)
        {
            string requestUrl = $"{GetTicketApiUrl()}/by-movie/{movieId}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            await ThrowExceptionOnErrorAsync(httpResponse);

            string responseBodyJson = await httpResponse.Content.ReadAsStringAsync();

            TicketSerializationModel[] ticketSerializationModels = jsonSerializer.Deserialize<TicketSerializationModel[]>(responseBodyJson);
            return ticketSerializationModels.Select(ticketSerializer.Deserialize).ToList();
        }

        public async Task<IReadOnlyCollection<ITicket>> GetByUserEmailAsync(string userEmail)
        {
            string requestUrl = $"{GetTicketApiUrl()}/by-user";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(userEmail);

            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            await ThrowExceptionOnErrorAsync(httpResponse);

            string responseBodyJson = await httpResponse.Content.ReadAsStringAsync();

            TicketSerializationModel[] ticketSerializationModels = jsonSerializer.Deserialize<TicketSerializationModel[]>(responseBodyJson);
            return ticketSerializationModels.Select(ticketSerializer.Deserialize).ToList();
        }

        public async Task<int> AddAsync(TicketCreationModel ticketCreationModel)
        {
            string requestUrl = $"{GetTicketApiUrl()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(ticketCreationModel));

            HttpClient httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            await ThrowExceptionOnErrorAsync(httpResponse);

            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            return int.Parse(responseBody);
        }

        public async Task OrderAsync(OrderModel orderModel)
        {
            string requestUrl = $"{GetTicketApiUrl()}/order";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(orderModel));

            HttpClient httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(httpRequest);

            await ThrowExceptionOnErrorAsync(response);
        }

        private static async Task ThrowExceptionOnErrorAsync(HttpResponseMessage httpResponse)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.NotFound: throw new NotFoundException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: throw new TicketAlreadyOrderedException(await httpResponse.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: throw new InternalServiceException();
            }
        }

        private string GetTicketApiUrl()
        {
            string ticketServiceUrl = configuration.GetValue<string>("TicketFlow:TicketService:Url");
            return $"{ticketServiceUrl}/tickets";
        }
    }
}