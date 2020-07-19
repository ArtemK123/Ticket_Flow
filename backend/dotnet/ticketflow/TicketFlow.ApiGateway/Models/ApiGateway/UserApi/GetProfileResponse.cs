namespace TicketFlow.ApiGateway.Models.ApiGateway.UserApi
{
    public class GetProfileResponse
    {
        public GetProfileResponse(string userEmail, ProfileClientModel profile)
        {
            UserEmail = userEmail;
            Profile = profile;
        }

        public string UserEmail { get; }

        public ProfileClientModel Profile { get; }
    }
}