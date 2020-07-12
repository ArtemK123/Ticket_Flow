namespace ApiGateway.Models.ApiGateway.UserApi
{
    public class RegisterRequest
    {
        public RegisterRequest(string email, string password, ProfileClientModel profile)
        {
            Email = email;
            Password = password;
            Profile = profile;
        }

        public string Email { get; }

        public string Password { get; }

        public ProfileClientModel Profile { get; }
    }
}