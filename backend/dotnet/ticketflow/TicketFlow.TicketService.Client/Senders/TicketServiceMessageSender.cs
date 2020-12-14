using System;
using System.Net.Http;
using TicketFlow.Common.Senders;
using TicketFlow.Common.Serializers;
using TicketFlow.TicketService.Client.Validators;

namespace TicketFlow.TicketService.Client.Senders
{
    internal class TicketServiceMessageSender : ServiceApiMessageSenderBase, ITicketServiceMessageSender
    {
        public TicketServiceMessageSender(ITicketServiceResponseValidator responseValidator, Lazy<IHttpClientFactory> httpClientFactoryLazy, IJsonSerializer jsonSerializer)
            : base(responseValidator, httpClientFactoryLazy, jsonSerializer)
        {
        }
    }
}