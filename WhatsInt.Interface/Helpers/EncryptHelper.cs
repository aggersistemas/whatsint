using System.Security.Cryptography;
using System.Text;

namespace WhatsInt.Interface.Helpers
{
    public static class EncryptHelper
    {
        private static readonly byte[] Salt = { 0x57, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc8, 0xfe, 0x09, 0xaf, 0x3d, 0x01, 0x12, 0x7c };

        private const string EncryptionKey = "6s!bH8R&$6mg";

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
    }
}
