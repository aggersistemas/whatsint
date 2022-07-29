using System.Security.Cryptography;
using System.Text;

namespace WhatsInt.Common.Helpers
{
    public static class EncryptionHelper
    {
        public static string Decrypt(this string? cipher)
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

        public static string Base64Encode(this string? plainText)
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
