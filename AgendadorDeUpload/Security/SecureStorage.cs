using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AgendadorDeUpload.Security
{
    public static class SecureStorage
    {
        private static readonly byte[] AppSalt = Encoding.UTF8.GetBytes("AgendadorDeUpload_v2");

        public static string Encrypt(string plainText, string password)
        {
            var aesEncrypted = AesEncrypt(plainText, password);
            var dpapiProtected = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(aesEncrypted),
                AppSalt,
                DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(dpapiProtected);
        }

        public static string Decrypt(string cipherText, string password)
        {
            try
            {
                var dpapiUnprotected = ProtectedData.Unprotect(
                    Convert.FromBase64String(cipherText),
                    AppSalt,
                    DataProtectionScope.CurrentUser);
                var dpapiResult = Encoding.UTF8.GetString(dpapiUnprotected);
                return AesDecrypt(dpapiResult, password);
            }
            catch
            {
                return null;
            }
        }

        private static string AesEncrypt(string plainText, string password)
        {
            using (var aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(password, AppSalt, 100000);
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

        private static string AesDecrypt(string cipherText, string password)
        {
            using (var aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(password, AppSalt, 100000);
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
