using NSubstitute;
using TicketFlow.IdentityService.Client.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Models;
using TicketFlow.IdentityService.Client.Factories;
using TicketFlow.IdentityService.Client.Generators;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Factories
{
    public class UserFactoryTest
    {
        private const string Email = "test@gmail.com";
        private const Role UserRole = Role.User;
        private const string Password = "secret";
        private const string Token = "jwt";

        private readonly UserFactory userFactory;

        public UserFactoryTest()
        {
            userFactory = new UserFactory(Substitute.For<IJwtGenerator>());
        }

        [Fact]
        internal void Create_ShouldCreateUserFromUserCreationModel()
        {
            UserCreationModel creationModel = new UserCreationModel(Email, Password, UserRole);
            IUser actual = userFactory.Create(creationModel);

            Assert.Equal(creationModel.Email, actual.Email);
            Assert.Equal(creationModel.Password, actual.Password);
            Assert.Equal(creationModel.Role, actual.Role);
        }

        [Fact]
        internal void Create_ShouldCreateAuthorizedUserFromAuthorizedUserCreationModel()
        {
            AuthorizedUserCreationModel creationModel = new AuthorizedUserCreationModel(Email, Password, UserRole, Token);
            IAuthorizedUser actual = userFactory.Create(creationModel);

            Assert.Equal(creationModel.Email, actual.Email);
            Assert.Equal(creationModel.Password, actual.Password);
            Assert.Equal(creationModel.Role, actual.Role);
            Assert.Equal(creationModel.Token, actual.Token);
        }
    }
}