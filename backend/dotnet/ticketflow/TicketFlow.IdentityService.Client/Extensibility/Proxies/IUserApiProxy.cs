using System.Threading.Tasks;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;

namespace TicketFlow.IdentityService.Client.Extensibility.Proxies
{
    public interface IUserApiProxy
    {
        public Task<IAuthorizedUser> GetByTokenAsync(string token);

        public Task<IUser> GetByEmailAsync(string email);

        public Task<string> LoginAsync(LoginRequest loginRequest);

        public Task<string> RegisterAsync(RegisterRequest registerRequest);

        public Task<string> LogoutAsync(string token);
    }
}