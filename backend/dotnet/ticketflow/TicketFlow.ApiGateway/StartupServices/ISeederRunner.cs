using System.Threading.Tasks;

namespace TicketFlow.ApiGateway.StartupServices
{
    internal interface ISeederRunner
    {
        public Task RunSeedersAsync();
    }
}