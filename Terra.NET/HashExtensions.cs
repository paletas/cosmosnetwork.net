namespace Terra.NET
{
    internal static class HashExtensions
    {
        public static string HashToHex(string data)
        {
            return Convert.ToHexString(SHA256(Convert.FromBase64String(data)));
        }

        public static byte[] SHA256(byte[] data)
        {
            return Cryptography.ECDSA.Sha256Manager.GetHash(data);
        }
    }
}
