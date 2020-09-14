using TicketFlow.Common.Factories;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;

namespace TicketFlow.IdentityService.Client.Extensibility.Factories
{
    public interface IUserFactory : IEntityFactory<IUser, UserCreationModel>, IEntityFactory<IAuthorizedUser, AuthorizedUserCreationModel>
    {
    }
}