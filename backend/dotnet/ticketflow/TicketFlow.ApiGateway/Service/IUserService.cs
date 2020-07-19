using TicketFlow.ApiGateway.Models.ApiGateway.UserApi;

namespace TicketFlow.ApiGateway.Service
{
    public interface IUserService
    {
        void Register(RegisterRequest request);

        GetProfileResponse GetProfile(string token);
    }
}