using Domain.Interfaces.Token;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ITokenGenerator tokenGenerator;
        IConfiguration configuration;

        public RefreshTokenService(ITokenGenerator tokenGenerator, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.tokenGenerator = tokenGenerator;
        }

        public string Generate(User user)
        {
            var RefreshTokenExpirationMinutesString = this.configuration["AppSettings:RefreshTokenExpirationMinutes"];
            var RefreshTokenExpirationMinutes = double.Parse(RefreshTokenExpirationMinutesString, new NumberFormatInfo { NumberDecimalSeparator = "." });
            return this.tokenGenerator.Generate(Encoding.UTF8.GetBytes(this.configuration.GetSection("AppSettings:AccessTokenSecret").Value), RefreshTokenExpirationMinutes);
        }
    }
}
