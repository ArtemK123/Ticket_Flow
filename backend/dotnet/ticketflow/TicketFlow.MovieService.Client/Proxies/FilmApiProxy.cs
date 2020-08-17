using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.Common.Serializers;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Client.Providers;
using TicketFlow.MovieService.Client.Senders;

namespace TicketFlow.MovieService.Client.Proxies
{
    internal class FilmApiProxy : IFilmApiProxy
    {
        private readonly IFilmSerializer filmSerializer;
        private readonly IMovieServiceMessageSender serviceMessageSender;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMovieServiceUrlProvider movieServiceUrlProvider;

        public FilmApiProxy(IFilmSerializer filmSerializer, IMovieServiceMessageSender serviceMessageSender, IJsonSerializer jsonSerializer, IMovieServiceUrlProvider movieServiceUrlProvider)
        {
            this.filmSerializer = filmSerializer;
            this.serviceMessageSender = serviceMessageSender;
            this.jsonSerializer = jsonSerializer;
            this.movieServiceUrlProvider = movieServiceUrlProvider;
        }

        public async Task<IReadOnlyCollection<IFilm>> GetAllAsync()
        {
            string requestUrl = $"{GetApiUrl()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            IReadOnlyCollection<FilmSerializationModel> filmSerializationModels = await serviceMessageSender.SendAsync<FilmSerializationModel[]>(httpRequest);

            return filmSerializationModels.Select(filmSerializer.Deserialize).ToList();
        }

        public async Task<int> AddAsync(FilmCreationModel filmCreationModel)
        {
            string requestUrl = $"{GetApiUrl()}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new StringContent(jsonSerializer.Serialize(filmCreationModel));

            return await serviceMessageSender.SendAsync<int>(httpRequest);
        }

        private string GetApiUrl() => $"{movieServiceUrlProvider.GetUrl()}/films";
    }
}