using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicketFlow.Common.Serializers;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Client.Providers;
using TicketFlow.MovieService.Client.Senders;

namespace TicketFlow.MovieService.Client.Proxies
{
    internal class MovieApiProxy : IMovieApiProxy
    {
        private readonly IMovieSerializer movieSerializer;
        private readonly IMovieServiceMessageSender serviceMessageSender;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMovieServiceUrlProvider movieServiceUrlProvider;

        public MovieApiProxy(
            IMovieSerializer movieSerializer,
            IMovieServiceMessageSender serviceMessageSender,
            IJsonSerializer jsonSerializer,
            IMovieServiceUrlProvider movieServiceUrlProvider)
        {
            this.movieSerializer = movieSerializer;
            this.serviceMessageSender = serviceMessageSender;
            this.jsonSerializer = jsonSerializer;
            this.movieServiceUrlProvider = movieServiceUrlProvider;
        }

        public async Task<IReadOnlyCollection<IMovie>> GetAllAsync()
        {
            string requestUrl = await FormServiceUrl();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            IReadOnlyCollection<MovieSerializationModel> movieSerializationModels = await serviceMessageSender.SendAsync<MovieSerializationModel[]>(httpRequest);

            return movieSerializationModels.Select(movieSerializer.Deserialize).ToList();
        }

        public async Task<IMovie> GetByIdAsync(int id)
        {
            string requestUrl = await FormServiceUrl($"/{id}");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            MovieSerializationModel serializationModel = await serviceMessageSender.SendAsync<MovieSerializationModel>(httpRequest);

            return movieSerializer.Deserialize(serializationModel);
        }

        public async Task<int> AddAsync(MovieCreationIdReferencedModel movieCreationIdReferencedModel)
        {
            string requestUrl = await FormServiceUrl();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(movieCreationIdReferencedModel), Encoding.UTF8, "application/json");

            return await serviceMessageSender.SendAsync(httpRequest, int.Parse);
        }

        private async Task<string> FormServiceUrl(string requestUrl = "")
        {
            string serviceUrl = await movieServiceUrlProvider.GetUrlAsync();
            return $"{serviceUrl}/movies{requestUrl}";
        }
    }
}