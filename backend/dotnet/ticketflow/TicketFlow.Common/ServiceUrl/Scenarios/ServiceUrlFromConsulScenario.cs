using System;
using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Scenarios
{
    internal class ServiceUrlFromConsulScenario : IServiceUrlProvidingScenario
    {
        public ServiceUrlProvidingType ProvidingType => ServiceUrlProvidingType.FromConsul;

        public string GetUrl(string serviceName)
        {
            throw new NotImplementedException();
        }
    }
}