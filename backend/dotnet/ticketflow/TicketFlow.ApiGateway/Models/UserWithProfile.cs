using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.ProfileService.Client.Extensibility.Entities;

namespace TicketFlow.ApiGateway.Models
{
    public class UserWithProfile
    {
        public UserWithProfile(IUser user, IProfile profile)
        {
            User = user;
            Profile = profile;
        }

        public IUser User { get; }

        public IProfile Profile { get; }
    }
}