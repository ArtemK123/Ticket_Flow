using System.Threading.Tasks;
using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Scenarios
{
    internal interface IServiceUrlProvidingScenario
    {
        ServiceUrlProvidingType ProvidingType { get; }

        Task<string> GetUrlAsync(string serviceName);
    }
}