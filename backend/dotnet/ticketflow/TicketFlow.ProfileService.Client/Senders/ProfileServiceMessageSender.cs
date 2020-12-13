using System;
using System.Net.Http;
using TicketFlow.Common.Senders;
using TicketFlow.Common.Serializers;
using TicketFlow.ProfileService.Client.Validators;

namespace TicketFlow.ProfileService.Client.Senders
{
    internal class ProfileServiceMessageSender : ServiceApiMessageSenderBase, IProfileServiceMessageSender
    {
        public ProfileServiceMessageSender(IProfileServiceResponseValidator responseValidator, Lazy<IHttpClientFactory> httpClientFactoryLazy, IJsonSerializer jsonSerializer)
            : base(responseValidator, httpClientFactoryLazy, jsonSerializer)
        {
        }
    }
}