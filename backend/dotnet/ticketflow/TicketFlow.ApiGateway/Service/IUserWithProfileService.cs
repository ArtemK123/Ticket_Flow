using System.Threading.Tasks;
using TicketFlow.ApiGateway.Models;

namespace TicketFlow.ApiGateway.Service
{
    public interface IUserWithProfileService
    {
        Task RegisterAsync(UserWithProfile userWithProfile);

        Task<UserWithProfile> GetUserWithProfileAsync(string token);
    }
}