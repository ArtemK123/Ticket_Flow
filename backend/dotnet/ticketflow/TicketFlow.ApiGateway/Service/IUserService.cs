using TicketFlow.ApiGateway.Models;
using TicketFlow.IdentityService.Client.Extensibility.Models;

namespace TicketFlow.ApiGateway.Service
{
    public interface IUserService
    {
        string Login(LoginRequest loginRequest);

        string Register(UserWithProfile userWithProfile);

        UserWithProfile GetUserWithProfile(string token);

        void Logout(string token);
    }
}