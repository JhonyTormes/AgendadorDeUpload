using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AgendadorDeUpload.Security
{
    public static class SecureStorage
    {
        private const int SaltSize = 16;
        private const int IvSize = 16;
        private const int Pbkdf2Iterations = 300000;

        public static string Encrypt(string plainText, string password)
        {
            var salt = new byte[SaltSize];
            var iv = new byte[IvSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                rng.GetBytes(iv);
            }

            byte[] ciphertext;
            using (var aes = Aes.Create())
            {
                using (var keyDerivation = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256))
                {
                    aes.Key = keyDerivation.GetBytes(32);
                }
                aes.IV = iv;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    ciphertext = ms.ToArray();
                }
            }

            var combined = new byte[SaltSize + IvSize + ciphertext.Length];
            Buffer.BlockCopy(salt, 0, combined, 0, SaltSize);
            Buffer.BlockCopy(iv, 0, combined, SaltSize, IvSize);
            Buffer.BlockCopy(ciphertext, 0, combined, SaltSize + IvSize, ciphertext.Length);

            return Convert.ToBase64String(combined);
        }

        public static string Decrypt(string cipherText, string password)
        {
            try
            {
                var data = Convert.FromBase64String(cipherText);

                if (data.Length <= SaltSize + IvSize)
                    return null;

                var salt = new byte[SaltSize];
                var iv = new byte[IvSize];
                Buffer.BlockCopy(data, 0, salt, 0, SaltSize);
                Buffer.BlockCopy(data, SaltSize, iv, 0, IvSize);

                var ciphertext = new byte[data.Length - SaltSize - IvSize];
                Buffer.BlockCopy(data, SaltSize + IvSize, ciphertext, 0, ciphertext.Length);

                using (var aes = Aes.Create())
                {
                    using (var keyDerivation = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256))
                    {
                        aes.Key = keyDerivation.GetBytes(32);
                    }
                    aes.IV = iv;

                    using (var ms = new MemoryStream(ciphertext))
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static void SaveToFile(string filePath, string encryptedData)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(filePath, encryptedData);
        }

        public static string LoadFromFile(string filePath)
        {
            return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
        }

        public static string GetDefaultSettingsPath()
        {
            var exeDir = Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(exeDir ?? ".", "settings.enc");
        }
    }
}
