using TicketFlow.Common.Factories;
using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Domain.Models;

namespace TicketFlow.IdentityService.Service.Factories
{
    internal class UserFactory : IEntityFactory<IUser, UserCreationModel>
    {
        private readonly IJwtGenerator jwtGenerator;

        public UserFactory(IJwtGenerator jwtGenerator)
        {
            this.jwtGenerator = jwtGenerator;
        }

        public IUser Create(UserCreationModel creationModel)
        {
            return new User(creationModel.Email, creationModel.Role, creationModel.Password, jwtGenerator);
        }
    }
}