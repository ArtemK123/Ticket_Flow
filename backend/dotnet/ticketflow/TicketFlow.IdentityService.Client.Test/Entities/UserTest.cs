using NSubstitute;
using TicketFlow.IdentityService.Client.Entities;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Generators;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Entities
{
    public class UserTest
    {
        private const string Email = "test@gmail.com";
        private const Role UserRole = Role.User;
        private const string Password = "secret";
        private const string InvalidPassword = "invalid";
        private const string Token = "jwt";

        private readonly IJwtGenerator jwtGeneratorMock;
        private readonly User user;

        public UserTest()
        {
            jwtGeneratorMock = CreateMockedJwtGenerator();
            user = new User(Email, UserRole, Password, jwtGeneratorMock);
        }

        [Theory]
        [InlineData(Password, true)]
        [InlineData(InvalidPassword, false)]
        internal void TryAuthorize_WhenRightPassword_ShouldReturnTrue(string password, bool expected)
        {
            var actual = user.TryAuthorize(password, out _);
            Assert.Equal(expected, actual);
        }

        [Fact]
        internal void TryAuthorize_RightPassword_ShouldCreateAuthorizedUserBasedOnGivenUser()
        {
            user.TryAuthorize(Password, out IAuthorizedUser authorizedUser);

            Assert.Equal(user.Email, authorizedUser.Email);
            Assert.Equal(user.Password, authorizedUser.Password);
            Assert.Equal(user.Role, authorizedUser.Role);
        }

        [Fact]
        internal void TryAuthorize_RightPassword_ShouldCreateAuthorizedUserWithTokenFromJwtGenerator()
        {
            user.TryAuthorize(Password, out IAuthorizedUser authorizedUser);

            jwtGeneratorMock.Received().Generate(user);
            Assert.Equal(Token, authorizedUser.Token);
        }

        [Fact]
        internal void TryAuthorize_WrongPassword_ShouldReturnDefaultUser()
        {
            user.TryAuthorize(InvalidPassword, out IAuthorizedUser authorizedUser);

            Assert.Equal(default, authorizedUser);
        }

        private static IJwtGenerator CreateMockedJwtGenerator()
        {
            var mock = Substitute.For<IJwtGenerator>();
            mock.Generate(null).ReturnsForAnyArgs(Token);
            return mock;
        }
    }
}