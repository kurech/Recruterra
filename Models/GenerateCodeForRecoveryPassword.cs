using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class GenerateCodeForRecoveryPassword
    {
        public static string CurentCode = string.Empty;
        public static string Generate()
        {
            Random random = new Random();
            string code = random.Next(0, 9).ToString() + random.Next(0, 9).ToString() + random.Next(0, 9).ToString() + random.Next(0, 9).ToString();
            CurentCode = code;
            return code;
        }
    }
}
