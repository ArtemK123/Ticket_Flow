using System.Threading.Tasks;
using TicketFlow.Common.ServiceUrl.Providers;

namespace TicketFlow.IdentityService.Client.Providers
{
    internal class IdentityServiceUrlProvider : IIdentityServiceUrlProvider
    {
        private readonly IServiceUrlProvider serviceUrlProvider;

        public IdentityServiceUrlProvider(IServiceUrlProvider serviceUrlProvider)
        {
            this.serviceUrlProvider = serviceUrlProvider;
        }

        public Task<string> GetUrlAsync() => serviceUrlProvider.GetUrlAsync("IdentityService");
    }
}