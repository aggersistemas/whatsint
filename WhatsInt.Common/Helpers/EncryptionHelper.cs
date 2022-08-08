using System.Security.Cryptography;
using System.Text;

namespace WhatsInt.Common.Helpers
{
    public static class EncryptionHelper
    {
        public const string Secret = "e0113b9e-4440-4d99-8c89-de4eb5506595";

        private static readonly byte[] Salt = { 0x57, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc8, 0xfe, 0x09, 0xaf, 0x3d, 0x01, 0x12, 0x7c };

        private const string EncryptionKey = "6s!bH8R&$6mg";

        
    }
}
