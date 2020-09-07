using System.Threading.Tasks;

namespace TicketFlow.ApiGateway.Service
{
    public interface IOrderTicketUseCase
    {
        Task OrderAsync(int ticketId, string token);
    }
}