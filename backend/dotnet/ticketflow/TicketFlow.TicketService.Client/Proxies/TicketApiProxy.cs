using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicketFlow.Common.Serializers;
using TicketFlow.Common.ServiceUrl.Providers;
using TicketFlow.TicketService.Client.Extensibility.Entities;
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Proxies;
using TicketFlow.TicketService.Client.Extensibility.Serializers;
using TicketFlow.TicketService.Client.Senders;

namespace TicketFlow.TicketService.Client.Proxies
{
    internal class TicketApiProxy : ITicketApiProxy
    {
        private const string ServiceName = "TicketService";

        private readonly ITicketSerializer ticketSerializer;
        private readonly IJsonSerializer jsonSerializer;
        private readonly ITicketServiceMessageSender serviceMessageSender;
        private readonly IServiceUrlProvider serviceUrlProvider;

        public TicketApiProxy(
            ITicketSerializer ticketSerializer,
            IJsonSerializer jsonSerializer,
            ITicketServiceMessageSender serviceMessageSender,
            IServiceUrlProvider serviceUrlProvider)
        {
            this.ticketSerializer = ticketSerializer;
            this.jsonSerializer = jsonSerializer;
            this.serviceMessageSender = serviceMessageSender;
            this.serviceUrlProvider = serviceUrlProvider;
        }

        public async Task<IReadOnlyCollection<ITicket>> GetByMovieIdAsync(int movieId)
        {
            string requestUrl = $"{await GetTicketApiUrlAsync()}/by-movie/{movieId}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            TicketSerializationModel[] serializationModels = await serviceMessageSender.SendAsync<TicketSerializationModel[]>(httpRequest);
            return serializationModels.Select(ticketSerializer.Deserialize).ToList();
        }

        public async Task<IReadOnlyCollection<ITicket>> GetByUserEmailAsync(string userEmail)
        {
            string requestUrl = $"{await GetTicketApiUrlAsync()}/by-user";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(userEmail);

            TicketSerializationModel[] serializationModels = await serviceMessageSender.SendAsync<TicketSerializationModel[]>(httpRequest);
            return serializationModels.Select(ticketSerializer.Deserialize).ToList();
        }

        public async Task<int> AddAsync(TicketCreationModel ticketCreationModel)
        {
            string requestUrl = $"{await GetTicketApiUrlAsync()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(ticketCreationModel), Encoding.UTF8, "application/json");

            return await serviceMessageSender.SendAsync(httpRequest, int.Parse);
        }

        public async Task OrderAsync(OrderModel orderModel)
        {
            string requestUrl = $"{await GetTicketApiUrlAsync()}/order";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(orderModel), Encoding.UTF8, "application/json");

            await serviceMessageSender.SendAsync(httpRequest);
        }

        private async Task<string> GetTicketApiUrlAsync()
        {
            string ticketServiceUrl = await serviceUrlProvider.GetUrlAsync(ServiceName);
            return $"{ticketServiceUrl}/tickets";
        }
    }
}