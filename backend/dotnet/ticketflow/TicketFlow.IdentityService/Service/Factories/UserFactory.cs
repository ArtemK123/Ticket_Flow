using TicketFlow.IdentityService.Domain.Entities;
using TicketFlow.IdentityService.Domain.Models;

namespace TicketFlow.IdentityService.Service.Factories
{
    internal class UserFactory : IUserFactory
    {
        private readonly IJwtGenerator jwtGenerator;

        public UserFactory(IJwtGenerator jwtGenerator)
        {
            this.jwtGenerator = jwtGenerator;
        }

        public IUser Create(UserCreationModel creationModel)
            => new User(creationModel.Email, creationModel.Role, creationModel.Password, jwtGenerator);

        public IAuthorizedUser Create(AuthorizedUserCreationModel creationModel)
            => new AuthorizedUser(creationModel.Email, creationModel.Role, creationModel.Password, jwtGenerator, creationModel.Token);
    }
}