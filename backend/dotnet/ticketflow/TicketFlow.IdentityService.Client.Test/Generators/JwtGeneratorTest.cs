using System;
using System.Collections.Generic;
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

        private static readonly DateTime CurrentDateTime = DateTime.Now;
        private readonly IUser userMock;
        private readonly IDateTimeProvider dateTimeProviderMock;

        private readonly JwtGenerator jwtGenerator;

        public JwtGeneratorTest()
        {
            userMock = CreateMockedUser(UserEmail);
            dateTimeProviderMock = CreateMockedDateTimeProvider(CurrentDateTime);
            jwtGenerator = new JwtGenerator(dateTimeProviderMock);
        }

        public static IEnumerable<object[]> TokensDifferBasedOnExpireDateTestData => new[]
        {
            new object[] { CurrentDateTime, CurrentDateTime, true },
            new object[] { CurrentDateTime, CurrentDateTime.AddTicks(1), true },
            new object[] { CurrentDateTime, CurrentDateTime.AddMilliseconds(1), true },
            new object[] { CurrentDateTime, CurrentDateTime.AddSeconds(1), false }
        };

        [Fact]
        internal void Generate_Claims_ShouldContainClaimWithUserEmail()
        {
            string actualJwtToken = jwtGenerator.Generate(userMock);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);
            Claim nameIdentifierClaim = parsedToken.Claims.SingleOrDefault(claim => claim.Type == UserEmailClaimType);
            Assert.Equal(UserEmail, nameIdentifierClaim?.Value);
        }

        [Fact]
        internal void Generate_ExpiresDate_ShouldUseDateTimeFromDateTimeProvider()
        {
            jwtGenerator.Generate(userMock);
            dateTimeProviderMock.Received().GetCurrentUtcDateTime();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        internal void Generate_ExpiresDate_ShouldAddDaysToDateTimeFromProvider(ushort daysToAdd)
        {
            string actualJwtToken = jwtGenerator.Generate(userMock, daysToAdd);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);

            DateTime expectedExpiresDate = CurrentDateTime.AddDays(daysToAdd).Date;
            DateTime actualExpiredDate = parsedToken.ValidTo.Date;

            Assert.Equal(expectedExpiresDate, actualExpiredDate);
        }

        [Fact]
        internal void Generate_ExpiresDate_ByDefault_ShouldExpireAfterSevenDays()
        {
            string actualJwtToken = jwtGenerator.Generate(userMock);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);

            DateTime expectedExpiresDate = CurrentDateTime.AddDays(7).Date;
            DateTime actualExpiredDate = parsedToken.ValidTo.Date;

            Assert.Equal(expectedExpiresDate, actualExpiredDate);
        }

        [Fact]
        internal void Generate_Issuer_ShouldAddIssuer()
        {
            string actualJwtToken = jwtGenerator.Generate(userMock);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);

            string expectedIssuer = "IdentityService.JwtGenerator";

            Assert.Equal(expectedIssuer, parsedToken.Issuer);
        }

        [Fact]
        internal void Generate_SecurityAlgorithms_ShouldUseHS256Algorithm()
        {
            string actualJwtToken = jwtGenerator.Generate(userMock);
            JwtSecurityToken parsedToken = ParseJwtSecurityToken(actualJwtToken);

            const string expectedAlgorithmName = "HS256";

            Assert.Equal(expectedAlgorithmName, parsedToken.SignatureAlgorithm);
        }

        [Theory]
        [MemberData(nameof(TokensDifferBasedOnExpireDateTestData))]
        internal void Generate_AnotherCreation_IfExpireDatesDifferEnough_ShouldGenerateTwoDifferentTokens(DateTime firstDateTime, DateTime secondDateTime, bool areTokensEqual)
        {
            dateTimeProviderMock.GetCurrentUtcDateTime().Returns(firstDateTime);
            string token1 = jwtGenerator.Generate(userMock);

            dateTimeProviderMock.GetCurrentUtcDateTime().Returns(secondDateTime);
            string token2 = jwtGenerator.Generate(userMock);

            Assert.Equal(areTokensEqual, token1.Equals(token2));
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