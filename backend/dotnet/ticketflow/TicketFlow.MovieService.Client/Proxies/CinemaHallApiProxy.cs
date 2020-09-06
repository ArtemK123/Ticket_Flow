using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicketFlow.Common.Serializers;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Client.Providers;
using TicketFlow.MovieService.Client.Senders;

namespace TicketFlow.MovieService.Client.Proxies
{
    internal class CinemaHallApiProxy : ICinemaHallApiProxy
    {
        private readonly ICinemaHallSerializer cinemaHallSerializer;
        private readonly IMovieServiceMessageSender serviceMessageSender;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMovieServiceUrlProvider movieServiceUrlProvider;

        public CinemaHallApiProxy(
            ICinemaHallSerializer cinemaHallSerializer,
            IMovieServiceMessageSender serviceMessageSender,
            IJsonSerializer jsonSerializer,
            IMovieServiceUrlProvider movieServiceUrlProvider)
        {
            this.cinemaHallSerializer = cinemaHallSerializer;
            this.serviceMessageSender = serviceMessageSender;
            this.jsonSerializer = jsonSerializer;
            this.movieServiceUrlProvider = movieServiceUrlProvider;
        }

        public async Task<IReadOnlyCollection<ICinemaHall>> GetAllAsync()
        {
            string requestUrl = $"{GetApiUrl()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            IReadOnlyCollection<CinemaHallSerializationModel> cinemaHallSerializationModels = await serviceMessageSender.SendAsync<CinemaHallSerializationModel[]>(httpRequest);

            return cinemaHallSerializationModels.Select(cinemaHallSerializer.Deserialize).ToList();
        }

        public async Task<int> AddAsync(CinemaHallCreationModel cinemaHallCreationModel)
        {
            string requestUrl = $"{GetApiUrl()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(cinemaHallCreationModel), Encoding.UTF8, "application/json");

            return await serviceMessageSender.SendAsync(httpRequest, int.Parse);
        }

        private string GetApiUrl() => $"{movieServiceUrlProvider.GetUrl()}/cinemaHalls";
    }
}