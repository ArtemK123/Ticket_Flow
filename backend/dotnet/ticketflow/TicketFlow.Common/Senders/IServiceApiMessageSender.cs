using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TicketFlow.Common.Senders
{
    public interface IServiceApiMessageSender
    {
        Task<T> SendAsync<T>(HttpRequestMessage httpRequestMessage, Func<string, T> convertFunc = null);

        Task SendAsync(HttpRequestMessage httpRequestMessage);
    }
}