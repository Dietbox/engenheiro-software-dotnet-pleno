using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Utils
{
    public static class StringsMethods
    {
        public static string ToHash256(this string value)
        {
            SHA256 sha1 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(value);
            byte[] hash = sha1.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++) { sb.Append(hash[i].ToString("X2")); }
            return sb.ToString();
        }
    }
}
