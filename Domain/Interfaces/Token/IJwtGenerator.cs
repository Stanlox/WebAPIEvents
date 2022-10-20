using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Token
{
    public interface IJwtGenerator
    {
        public string CreateToken(User user);
    }
}
