using System.Threading.Tasks;

namespace TicketFlow.ApiGateway.Service
{
    public interface IOrderTicketRequestHandler
    {
        Task OrderAsync(int ticketId, string token);
    }
}