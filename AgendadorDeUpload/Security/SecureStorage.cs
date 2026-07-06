using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AgendadorDeUpload.Security
{
    public static class SecureStorage
    {
        private const int SaltSize = 16;
        private const int Pbkdf2Iterations = 300000;
        private static readonly byte[] LegacySalt = Encoding.UTF8.GetBytes("AgendadorDeUpload_v2");

        public static string Encrypt(string plainText, string password)
        {
            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            var aesEncrypted = AesEncrypt(plainText, password, salt);
            var dpapiProtected = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(aesEncrypted),
                salt,
                DataProtectionScope.CurrentUser);

            var combined = new byte[SaltSize + dpapiProtected.Length];
            Buffer.BlockCopy(salt, 0, combined, 0, SaltSize);
            Buffer.BlockCopy(dpapiProtected, 0, combined, SaltSize, dpapiProtected.Length);

            return Convert.ToBase64String(combined);
        }

        public static string Decrypt(string cipherText, string password)
        {
            try
            {
                var data = Convert.FromBase64String(cipherText);

                if (data.Length > SaltSize)
                {
                    try
                    {
                        var salt = new byte[SaltSize];
                        Buffer.BlockCopy(data, 0, salt, 0, SaltSize);
                        var dpapiBlob = new byte[data.Length - SaltSize];
                        Buffer.BlockCopy(data, SaltSize, dpapiBlob, 0, dpapiBlob.Length);

                        var dpapiResult = ProtectedData.Unprotect(dpapiBlob, salt, DataProtectionScope.CurrentUser);
                        var aesResult = Encoding.UTF8.GetString(dpapiResult);
                        return AesDecrypt(aesResult, password, salt);
                    }
                    catch { }
                }

                var dpapiUnprotected = ProtectedData.Unprotect(
                    data,
                    LegacySalt,
                    DataProtectionScope.CurrentUser);
                var dpapiResult2 = Encoding.UTF8.GetString(dpapiUnprotected);
                return AesDecrypt(dpapiResult2, password, LegacySalt);
            }
            catch
            {
                return null;
            }
        }

        private static string AesEncrypt(string plainText, string password, byte[] salt)
        {
            using (var aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256);
                aes.Key = key.GetBytes(32);
                aes.IV = key.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private static string AesDecrypt(string cipherText, string password, byte[] salt)
        {
            using (var aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256);
                aes.Key = key.GetBytes(32);
                aes.IV = key.GetBytes(16);
                var buffer = Convert.FromBase64String(cipherText);
                using (var ms = new MemoryStream(buffer))
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
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
