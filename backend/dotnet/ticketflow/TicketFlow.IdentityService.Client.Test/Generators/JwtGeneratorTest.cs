using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using NSubstitute;
using TicketFlow.Common.Providers;
using TicketFlow.IdentityService.Client.Extensibility.Entities;
using TicketFlow.IdentityService.Client.Generators;
using Xunit;

namespace TicketFlow.IdentityService.Client.Test.Generators
{
    public class JwtGeneratorTest
    {
        private const string UserEmailClaimType = "nameid";
        private const string UserEmail = "test@email.com";

        private readonly DateTime dateTime = DateTime.Now;
        private readonly IUser userMock;
        private readonly IDateTimeProvider dateTimeProviderMock;

        private readonly JwtGenerator jwtGenerator;

        public JwtGeneratorTest()
        {
            userMock = CreateMockedUser(UserEmail);
            dateTimeProviderMock = CreateMockedDateTimeProvider(dateTime);
            jwtGenerator = new JwtGenerator(dateTimeProviderMock);
        }

        [Fact]
        public void Generate_Claims_ShouldContainClaimWithUserEmail()
        {
            string actualJwtToken = jwtGenerator.Generate(userMock);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);
            Claim nameIdentifierClaim = parsedToken.Claims.SingleOrDefault(claim => claim.Type == UserEmailClaimType);
            Assert.Equal(UserEmail, nameIdentifierClaim?.Value);
        }

        [Fact]
        public void Generate_ExpiresDate_ShouldUseDateTimeFromDateTimeProvider()
        {
            jwtGenerator.Generate(userMock);
            dateTimeProviderMock.Received().GetCurrentUtcDateTime();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Generate_ExpiresDate_ShouldAddDaysToDateTimeFromProvider(ushort daysToAdd)
        {
            string actualJwtToken = jwtGenerator.Generate(userMock, daysToAdd);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);

            DateTime expectedExpiresDate = dateTime.AddDays(daysToAdd).Date;
            DateTime actualExpiredDate = parsedToken.ValidTo.Date;

            Assert.Equal(expectedExpiresDate, actualExpiredDate);
        }

        [Fact]
        public void Generate_ExpiresDate_ByDefault_ShouldExpireAfterSevenDays()
        {
            string actualJwtToken = jwtGenerator.Generate(userMock);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);

            DateTime expectedExpiresDate = dateTime.AddDays(7).Date;
            DateTime actualExpiredDate = parsedToken.ValidTo.Date;

            Assert.Equal(expectedExpiresDate, actualExpiredDate);
        }

        [Fact]
        public void Generate_Issuer_ShouldAddIssuer()
        {
            string actualJwtToken = jwtGenerator.Generate(userMock);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);

            string expectedIssuer = "IdentityService.JwtGenerator";

            Assert.Equal(expectedIssuer, parsedToken.Issuer);
        }

        [Fact]
        public void Generate_SecurityAlgorithms_ShouldUseHS256Algorithm()
        {
            string actualJwtToken = jwtGenerator.Generate(userMock);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);

            const string expectedAlgorithmName = "HS256";

            Assert.Equal(expectedAlgorithmName, parsedToken.SignatureAlgorithm);
        }

        private IUser CreateMockedUser(string email)
        {
            var mock = Substitute.For<IUser>();
            mock.Email.Returns(email);
            return mock;
        }

        private IDateTimeProvider CreateMockedDateTimeProvider(DateTime returnedDateTime)
        {
            var mock = Substitute.For<IDateTimeProvider>();
            mock.GetCurrentUtcDateTime().Returns(returnedDateTime);
            return mock;
        }

        private JwtSecurityToken ParseJwtSecurityToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(token);
        }
    }
}