﻿using System;
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
            string code = random.Next(1000, 9999).ToString();
            CurentCode = code;
            return code;
        }
    }
}
