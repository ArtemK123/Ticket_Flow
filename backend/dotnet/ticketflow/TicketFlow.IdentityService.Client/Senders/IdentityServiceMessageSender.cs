using System.Net.Http;
using TicketFlow.Common.Senders;
using TicketFlow.Common.Serializers;
using TicketFlow.IdentityService.Client.Validators;

namespace TicketFlow.IdentityService.Client.Senders
{
    internal class IdentityServiceMessageSender : ServiceApiMessageSenderBase, IIdentityServiceMessageSender
    {
        public IdentityServiceMessageSender(IIdentityServiceResponseValidator responseValidator, IHttpClientFactory httpClientFactory, IJsonSerializer jsonSerializer)
            : base(responseValidator, httpClientFactory, jsonSerializer)
        {
        }
    }
}