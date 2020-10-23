using System.Threading.Tasks;

namespace TicketFlow.IdentityService.Client.Providers
{
    internal interface IIdentityServiceUrlProvider
    {
        Task<string> GetUrlAsync();
    }
}