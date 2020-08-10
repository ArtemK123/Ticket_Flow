using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.WebApi.ClientModels.Requests;

namespace TicketFlow.IdentityService.Service
{
    public interface IUserService
    {
        IAuthorizedUser GetByToken(string token);

        IUser GetByEmail(string email);

        string Login(LoginRequest loginRequest);

        void Register(RegisterRequest registerRequest);

        void Logout(string token);
    }
}