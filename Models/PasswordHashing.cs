using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class PasswordHashing
    {
        public static string GetHashString(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            SHA256Managed hashAlgoritm = new SHA256Managed();
            byte[] hash = hashAlgoritm.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
