using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace TechChallenge.Infrastructure.Security
{
    internal class EncryptService
    {
        public static string Encrypt(string value)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            string result = string.Empty;
            foreach (var item in hash)
            {
                result += item.ToString("X2"); //X2 -> hexadecimal
            }
            return result;
        }
    }
}
