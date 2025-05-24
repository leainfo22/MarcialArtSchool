using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarcialArtSchool.Core.Utils
{
    public class Hash
    {
        public static string HashPasswordWithSalt(string password, int saltSize = 16)
        {
            byte[] salt = GenerateSalt(saltSize);

            // 2. Compute the SHA-256 hash with the salt
            byte[] hashBytes = ComputeSHA256Hash(password, salt);

            // 3. Combine the salt and hash into a single byte array
            byte[] saltAndHashBytes = new byte[salt.Length + hashBytes.Length];
            Buffer.BlockCopy(salt, 0, saltAndHashBytes, 0, salt.Length);
            Buffer.BlockCopy(hashBytes, 0, saltAndHashBytes, salt.Length, hashBytes.Length);

            // 4. Encode the combined byte array to a Base64 string
            return Convert.ToBase64String(saltAndHashBytes);
        }

        private static byte[] GenerateSalt(int saltSize)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[saltSize];
                rng.GetBytes(salt);
                return salt;
            }
        }

        private static byte[] ComputeSHA256Hash(string input, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedInput = Encoding.UTF8.GetBytes(input).Concat(salt).ToArray();
                return sha256.ComputeHash(saltedInput);
            }
        }
    }
}
