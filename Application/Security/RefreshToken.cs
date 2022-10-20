using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Creted { get; set; } = DateTime.Now;

        public DateTime Expired { get; set; }
    }
}