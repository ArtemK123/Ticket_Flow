using NSubstitute;
using TicketFlow.IdentityService.Client.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Generators;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Entities
{
    public class AuthorizedUserTest
    {
        private const string Email = "test@gmail.com";
        private const Role UserRole = Role.User;
        private const string Password = "secret";
        private const string Token = "jwt";

        private readonly AuthorizedUser authorizedUser;

        public AuthorizedUserTest()
        {
            IJwtGenerator jwtGeneratorMock = Substitute.For<IJwtGenerator>();
            authorizedUser = new AuthorizedUser(Email, UserRole, Password, jwtGeneratorMock, Token);
        }

        [Fact]
        public void Token_ShouldReturnTokenFromConstructor()
        {
            Assert.Equal(Token, authorizedUser.Token);
        }

        [Fact]
        public void TryAuthorize_RightPassword_ShouldReturnUserWithTheSameToken()
        {
            authorizedUser.TryAuthorize(Password, out IAuthorizedUser reauthorizedUser);
            Assert.Equal(Token, reauthorizedUser.Token);
        }

        [Fact]
        public void TryAuthorize_RightPassword_ShouldCreateNewAuthorizedUser()
        {
            authorizedUser.TryAuthorize(Password, out IAuthorizedUser reauthorizedUser);
            Assert.NotSame(authorizedUser, reauthorizedUser);
        }
    }
}