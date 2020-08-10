using TicketFlow.Common.Factories;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Domain.Models;

namespace TicketFlow.IdentityService.Service.Factories
{
    public interface IUserFactory : IEntityFactory<IUser, UserCreationModel>, IEntityFactory<IAuthorizedUser, AuthorizedUserCreationModel>
    {
    }
}