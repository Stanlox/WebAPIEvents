using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Token
{
    public interface ITokenGenerator
    {
        string Generate(byte[] secretKey, double expires, IEnumerable<Claim> claims = null);
    }
}
