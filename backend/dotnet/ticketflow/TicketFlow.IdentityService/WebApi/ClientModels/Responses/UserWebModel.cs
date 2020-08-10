namespace TicketFlow.IdentityService.WebApi.ClientModels.Responses
{
    public class UserWebModel
    {
        public UserWebModel(string email, int roleNumber, string token)
        {
            Email = email;
            RoleNumber = roleNumber;
            Token = token;
        }

        public string Email { get; }

        public int RoleNumber { get; }

        public string Token { get; }
    }
}