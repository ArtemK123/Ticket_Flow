using TicketFlow.IdentityService.Api.ClientModels.Requests;
using TicketFlow.IdentityService.Service.Entities;

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