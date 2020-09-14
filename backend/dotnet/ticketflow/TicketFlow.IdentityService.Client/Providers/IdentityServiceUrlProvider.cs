using Microsoft.Extensions.Configuration;

namespace TicketFlow.IdentityService.Client.Providers
{
    internal class IdentityServiceUrlProvider : IIdentityServiceUrlProvider
    {
        private readonly IConfiguration configuration;

        public IdentityServiceUrlProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetUrl() => configuration.GetValue<string>("TicketFlow:IdentityService:Url");
    }
}