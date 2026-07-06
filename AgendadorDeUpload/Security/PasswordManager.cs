using System;
using System.Security.Cryptography;
using System.Text;

namespace AgendadorDeUpload.Security
{
    public static class PasswordManager
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 300000;

        public static string HashPassword(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
            {
                var salt = deriveBytes.Salt;
                var key = deriveBytes.GetBytes(KeySize);
                var result = new byte[salt.Length + key.Length];
                Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
                Buffer.BlockCopy(key, 0, result, salt.Length, key.Length);
                return Convert.ToBase64String(result);
            }
        }

        public static bool VerifyPassword(string password, string hash)
        {
            try
            {
                var hashBytes = Convert.FromBase64String(hash);
                if (hashBytes.Length != SaltSize + KeySize)
                    return false;

                var salt = new byte[SaltSize];
                var key = new byte[KeySize];
                Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);
                Buffer.BlockCopy(hashBytes, SaltSize, key, 0, KeySize);

                using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    var computedKey = deriveBytes.GetBytes(KeySize);
                    return FixedTimeEquals(key, computedKey);
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            var result = 0;
            for (int i = 0; i < a.Length; i++)
                result |= a[i] ^ b[i];

            return result == 0;
        }
    }
}
