using Microsoft.Extensions.Configuration;
using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Providers
{
    internal class ServiceUrlProvidingTypeProvider : IServiceUrlProvidingTypeProvider
    {
        private const string ServiceUrlProvidingTypeSettingPath = "TicketFlow:ServiceUrlProvidingType";
        private readonly IConfiguration configuration;

        public ServiceUrlProvidingTypeProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ServiceUrlProvidingType GetServiceUrlResolvingType()
        {
            return configuration.GetValue<ServiceUrlProvidingType>(ServiceUrlProvidingTypeSettingPath);
        }
    }
}