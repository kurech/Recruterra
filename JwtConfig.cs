using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
        public string CookieName { get; set; }
    }
}
