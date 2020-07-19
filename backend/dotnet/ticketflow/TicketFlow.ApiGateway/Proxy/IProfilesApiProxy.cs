using TicketFlow.ApiGateway.Models.ProfileService;

namespace TicketFlow.ApiGateway.Proxy
{
    internal interface IProfilesApiProxy
    {
        Profile GetById(int id);

        Profile GetByUserAsync(string userEmail);

        Profile Add(Profile profile);
    }
}