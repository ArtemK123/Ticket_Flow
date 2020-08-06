using TicketFlow.IdentityService.Entities;
using TicketFlow.IdentityService.WebApi.ClientModels.Requests;

namespace TicketFlow.IdentityService.Service
{
    public interface IUserService
    {
        User GetByToken(string token);

        User GetByEmail(string email);

        string Login(LoginRequest loginRequest);

        void Register(RegisterRequest registerRequest);

        void Logout(string token);
    }
}