using System.Security.Cryptography;
using System.Text;

namespace WhatsInt.Interface.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly byte[] Salt = { 0x57, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc8, 0xfe, 0x09, 0xaf, 0x3d, 0x01, 0x12, 0x7c };

        private const string EncryptionKey = "6s!bH8R&$6mg";
        public const string Secret = "e0113b9e-4440-4d99-8c89-de4eb5506595";

        public static string Encrypt(this string plain)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plain);

            var encryptedBytes = EncryptBytes(plainTextBytes);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static byte[] EncryptBytes(this byte[] plainTextBytes)
        {
            using var deriveBytes = new Rfc2898DeriveBytes(EncryptionKey, Salt, 1000);

            using var rijndael = Aes.Create();

            rijndael.Key = deriveBytes.GetBytes(32);

            rijndael.IV = deriveBytes.GetBytes(16);

            using var msEncrypt = new MemoryStream();

            using var csEncrypt = new CryptoStream(msEncrypt, rijndael.CreateEncryptor(), CryptoStreamMode.Write);

            csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);

            csEncrypt.FlushFinalBlock();

            return msEncrypt.ToArray();
        }

        public static string? Decrypt(this string? cipher)
        {
            try
            {
                var withoutCipher = string.IsNullOrEmpty(cipher);

                if (withoutCipher) return null;

                var plainTextBytes = Convert.FromBase64String(cipher!);

                var decryptedBytes = DecryptBytes(plainTextBytes);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static byte[] DecryptBytes(this byte[] plainTextBytes)
        {
            using var deriveBytes = new Rfc2898DeriveBytes(EncryptionKey, Salt, 1000);

            using var rijndael = Aes.Create();

            rijndael.Key = deriveBytes.GetBytes(32);

            rijndael.IV = deriveBytes.GetBytes(16);

            using var memoryStream = new MemoryStream();

            using var cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            cryptoStream.FlushFinalBlock();

            return memoryStream.ToArray();
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string plainText)
        {
            var fromBase64String = Convert.FromBase64String(plainText);

            return Encoding.UTF8.GetString(fromBase64String);
        }
    }
}
