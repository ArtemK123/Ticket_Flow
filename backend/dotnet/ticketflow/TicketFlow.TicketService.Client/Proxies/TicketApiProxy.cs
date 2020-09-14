using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.Serializers;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Proxies;
using TicketFlow.TicketService.Client.Extensibility.Serializers;
using TicketFlow.TicketService.Client.Senders;

namespace TicketFlow.TicketService.Client.Proxies
{
    internal class TicketApiProxy : ITicketApiProxy
    {
        private readonly ITicketSerializer ticketSerializer;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IConfiguration configuration;
        private readonly ITicketServiceMessageSender serviceMessageSender;

        public TicketApiProxy(
            IConfiguration configuration,
            ITicketSerializer ticketSerializer,
            IJsonSerializer jsonSerializer,
            ITicketServiceMessageSender serviceMessageSender)
        {
            this.configuration = configuration;
            this.ticketSerializer = ticketSerializer;
            this.jsonSerializer = jsonSerializer;
            this.serviceMessageSender = serviceMessageSender;
        }

        public async Task<IReadOnlyCollection<ITicket>> GetByMovieIdAsync(int movieId)
        {
            string requestUrl = $"{GetTicketApiUrl()}/by-movie/{movieId}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            TicketSerializationModel[] serializationModels = await serviceMessageSender.SendAsync<TicketSerializationModel[]>(httpRequest);
            return serializationModels.Select(ticketSerializer.Deserialize).ToList();
        }

        public async Task<IReadOnlyCollection<ITicket>> GetByUserEmailAsync(string userEmail)
        {
            string requestUrl = $"{GetTicketApiUrl()}/by-user";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(userEmail);

            TicketSerializationModel[] serializationModels = await serviceMessageSender.SendAsync<TicketSerializationModel[]>(httpRequest);
            return serializationModels.Select(ticketSerializer.Deserialize).ToList();
        }

        public async Task<int> AddAsync(TicketCreationModel ticketCreationModel)
        {
            string requestUrl = $"{GetTicketApiUrl()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(ticketCreationModel), Encoding.UTF8, "application/json");

            return await serviceMessageSender.SendAsync(httpRequest, int.Parse);
        }

        public async Task OrderAsync(OrderModel orderModel)
        {
            string requestUrl = $"{GetTicketApiUrl()}/order";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(orderModel), Encoding.UTF8, "application/json");

            await serviceMessageSender.SendAsync(httpRequest);
        }

        private string GetTicketApiUrl()
        {
            string ticketServiceUrl = configuration.GetValue<string>("TicketFlow:TicketService:Url");
            return $"{ticketServiceUrl}/tickets";
        }
    }
}