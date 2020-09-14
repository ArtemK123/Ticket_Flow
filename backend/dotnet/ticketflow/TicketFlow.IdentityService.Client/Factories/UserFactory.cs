using TicketFlow.IdentityService.Client.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Factories;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Generators;

namespace TicketFlow.IdentityService.Client.Factories
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