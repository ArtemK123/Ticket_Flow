using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TicketFlow.IdentityService.Client.Test.Senders.IdentityServiceMessageSenderTest
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage httpResponseMessage;

        public FakeHttpMessageHandler(HttpResponseMessage httpResponseMessage)
        {
            this.httpResponseMessage = httpResponseMessage;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(httpResponseMessage);
        }
    }
}