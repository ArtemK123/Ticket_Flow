using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TicketFlow.IdentityService.Entities;

namespace TicketFlow.IdentityService.Service
{
    internal class JwtGenerator : IJwtGenerator
    {
        private const int DefaultExpireDays = 7;
        private const string SecretKey = "top_secret";
        private const string JwtIssuerName = "IdentityService.JwtGenerator";

        private readonly IDateTimeProvider dateTimeProvider;

        public JwtGenerator(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public string Generate(User user)
        {
            return Generate(user, DefaultExpireDays);
        }

        public string Generate(User user, int expireDays)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
            DateTime expiresDate = dateTimeProvider.GetCurrentUtcDateTime().AddDays(expireDays);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                }),
                Expires = expiresDate,
                Issuer = JwtIssuerName,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}