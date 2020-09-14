using System.Threading.Tasks;

namespace TicketFlow.ApiGateway.StartupServices.Seeders
{
    public interface ISeeder
    {
        public Task SeedAsync();
    }
}