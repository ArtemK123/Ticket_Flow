using System.Net.Http;
using TicketFlow.Common.Senders;
using TicketFlow.Common.Serializers;
using TicketFlow.MovieService.Client.Validators;

namespace TicketFlow.MovieService.Client.Senders
{
    internal class MovieServiceMessageSender : ServiceApiMessageSenderBase, IMovieServiceMessageSender
    {
        public MovieServiceMessageSender(IMovieServiceResponseValidator responseValidator, IHttpClientFactory httpClientFactory, IJsonSerializer jsonSerializer)
            : base(responseValidator, httpClientFactory, jsonSerializer)
        {
        }
    }
}