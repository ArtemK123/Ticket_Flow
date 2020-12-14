using System;
using System.Net.Http;
using TicketFlow.Common.Senders;
using TicketFlow.Common.Serializers;
using TicketFlow.MovieService.Client.Validators;

namespace TicketFlow.MovieService.Client.Senders
{
    internal class MovieServiceMessageSender : ServiceApiMessageSenderBase, IMovieServiceMessageSender
    {
        public MovieServiceMessageSender(IMovieServiceResponseValidator responseValidator, Lazy<IHttpClientFactory> httpClientFactoryLazy, IJsonSerializer jsonSerializer)
            : base(responseValidator, httpClientFactoryLazy, jsonSerializer)
        {
        }
    }
}