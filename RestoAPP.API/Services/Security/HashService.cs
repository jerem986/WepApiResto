using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.API.Services.Security
{
    public class HashService
    {
        public string Hash(string password, string salt = null)
        {
            SHA512CryptoServiceProvider algo = new SHA512CryptoServiceProvider(); //accès a SHA512CryptoServiceProvider + import System.Security.Cryptography;
            byte[] toHash = Encoding.UTF8.GetBytes(password + (salt ?? string.Empty)); //passage du pwd + salt en utf8
            return Encoding.UTF8.GetString(algo.ComputeHash(toHash)); // on le retourne en string et avant on le hash?
        }
    }
}
