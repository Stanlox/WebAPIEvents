using Domain.Interfaces.Token;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        public string Generate(byte[] secretKey, double expires, IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                 expires: DateTime.UtcNow.AddMinutes(expires),
                 claims: claims,
                 signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
