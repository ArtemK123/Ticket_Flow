using ApiGateway.Models.ApiGateway.UserApi;

namespace ApiGateway.Service
{
    public interface IUserService
    {
        void Register(RegisterRequest request);

        GetProfileResponse GetProfile(string token);
    }
}