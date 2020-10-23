using System.Threading.Tasks;

namespace TicketFlow.Common.ServiceUrl.Providers
{
    public interface IServiceUrlProvider
    {
        Task<string> GetUrlAsync(string serviceName);
    }
}