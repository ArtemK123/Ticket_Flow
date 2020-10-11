using System;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Providers
{
    internal class ServiceUrlFromConsulProvider : IServiceUrlProvider
    {
        public ServiceUrlProvidingType ProvidingType { get; }
        public string GetUrl(string serviceName)
        {
            throw new NotImplementedException();
        }
    }
}