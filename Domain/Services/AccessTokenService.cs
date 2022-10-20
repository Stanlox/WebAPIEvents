using Domain.Interfaces.Token;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IConfiguration configuration;

        public AccessTokenService(ITokenGenerator tokenGenerator, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.tokenGenerator = tokenGenerator;
        }
        public string Generate(User user)
        {
            var authClaims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimsIdentity.DefaultRoleClaimType, user.role),
                };

            var AccessTokenExpirationMinutesString = this.configuration["AppSettings:AccessTokenExpirationMinutes"];
            var AccessTokenExpirationMinutes = double.Parse(AccessTokenExpirationMinutesString, new NumberFormatInfo { NumberDecimalSeparator = "." });
            return this.tokenGenerator.Generate(Encoding.UTF8.GetBytes(this.configuration.GetSection("AppSettings:AccessTokenSecret").Value), AccessTokenExpirationMinutes, authClaims);
        }
    }
}
