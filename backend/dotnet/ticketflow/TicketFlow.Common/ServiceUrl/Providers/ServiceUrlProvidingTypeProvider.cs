using System;
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
            string resolvingTypeAsString = configuration.GetValue<string>(ServiceUrlProvidingTypeSettingPath);

            if (Enum.TryParse(resolvingTypeAsString, false, out ServiceUrlProvidingType resolvingType))
            {
                return resolvingType;
            }

            return default;
        }
    }
}