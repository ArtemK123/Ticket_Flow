using System.Net.Http;
using TicketFlow.Common.Senders;
using TicketFlow.Common.Serializers;
using TicketFlow.ProfileService.Client.Validators;

namespace TicketFlow.ProfileService.Client.Senders
{
    internal class ProfileServiceMessageSender : ServiceApiMessageSenderBase, IProfileServiceMessageSender
    {
        public ProfileServiceMessageSender(IProfileServiceResponseValidator responseValidator, IHttpClientFactory httpClientFactory, IJsonSerializer jsonSerializer)
            : base(responseValidator, httpClientFactory, jsonSerializer)
        {
        }
    }
}