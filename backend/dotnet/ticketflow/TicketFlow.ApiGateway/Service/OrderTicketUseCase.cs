using System.Threading.Tasks;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Proxies;
using TicketFlow.TicketService.Client.Extensibility.Models;
using TicketFlow.TicketService.Client.Extensibility.Proxies;

namespace TicketFlow.ApiGateway.Service
{
    internal class OrderTicketUseCase : IOrderTicketUseCase
    {
        private readonly ITicketApiProxy ticketApiProxy;
        private readonly IUserApiProxy userApiProxy;

        public OrderTicketUseCase(ITicketApiProxy ticketApiProxy, IUserApiProxy userApiProxy)
        {
            this.ticketApiProxy = ticketApiProxy;
            this.userApiProxy = userApiProxy;
        }

        public async Task OrderAsync(int ticketId, string token)
        {
            IAuthorizedUser buyer = await userApiProxy.GetByTokenAsync(token);
            await ticketApiProxy.OrderAsync(new OrderModel(ticketId, buyer.Email));
        }
    }
}