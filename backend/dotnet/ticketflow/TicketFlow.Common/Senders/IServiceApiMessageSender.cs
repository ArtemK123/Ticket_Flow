using System.Net.Http;
using System.Threading.Tasks;

namespace TicketFlow.Common.Senders
{
    public interface IServiceApiMessageSender
    {
        Task<T> SendAsync<T>(HttpRequestMessage httpRequestMessage);

        Task SendAsync(HttpRequestMessage httpRequestMessage);
    }
}