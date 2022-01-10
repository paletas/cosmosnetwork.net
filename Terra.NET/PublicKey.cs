using DevHawk.Security.Cryptography;
using System.Security.Cryptography;

namespace Terra.NET
{
    public class PublicKey
    {
        private byte[] _key;

        public PublicKey(byte[] key)
        {
            this._key = key;
        }

        public byte[] RawKey => _key;

        public byte[] RawAddress
        {
            get
            {
                return new RIPEMD160().ComputeHash(SHA256.HashData(this._key));
            }
        }

        public string Address
        {
            get { return Converter.EncodeBech32("terra", this.RawAddress); }
        }
    }

    internal class Bech32ConversionException : Exception
    {
        public Bech32ConversionException()
            : base()
        {
        }
        public Bech32ConversionException(String message)
            : base(message)
        {
        }
    }

    internal static class Converter
    {
        private const string CHARSET_BECH32 = "qpzry9x8gf2tvdw0s3jn54khce6mua7l";

        private static int PolymodStep(int input)
        {
            int b = input >> 25;
            return ((input & 0x1ffffff) << 5) ^
                (-((b >> 0) & 1) & 0x3b6a57b2) ^
                (-((b >> 1) & 1) & 0x26508e6d) ^
                (-((b >> 2) & 1) & 0x1ea119fa) ^
                (-((b >> 3) & 1) & 0x3d4233dd) ^
                (-((b >> 4) & 1) & 0x2a1462b3);
        }

        private static int PrefixCheck(string prefix)
        {
            int chk = 1;
            for (var i = 0; i < prefix.Length; ++i)
            {
                var c = (byte)prefix[i];
                if (c < 33 || c > 126)
                    throw new ArgumentException($"Invalid prefix ({prefix})", nameof(prefix));
                chk = PolymodStep(chk) ^ (c >> 5);
            }
            chk = PolymodStep(chk);
            for (var i = 0; i < prefix.Length; ++i)
            {
                var v = prefix[i];
                chk = PolymodStep(chk) ^ (v & 0x1f);
            }
            return chk;
        }

        private static int[] ConvertBits(byte[] data, int inBits, int outBits, bool pad)
        {
            var value = 0;
            var bits = 0;
            var maxV = (1 << outBits) - 1;
            var result = new List<int>();
            for (var i = 0; i < data.Length; ++i)
            {
                value = (value << inBits) | data[i];
                bits += inBits;
                while (bits >= outBits)
                {
                    bits -= outBits;
                    result.Add((value >> bits) & maxV);
                }
            }
            if (pad)
            {
                if (bits > 0)
                {
                    result.Add((value << (outBits - bits)) & maxV);
                }
            }
            else
            {
                if (bits >= inBits)
                    throw new ArgumentException("Excess padding", nameof(inBits));
                if (((value << (outBits - bits)) & maxV) > 0)
                    throw new InvalidOperationException("Non-zero padding");
            }
            return result.ToArray();
        }

        // Witness = witness version byte + 2-40 byte witness program
        // witness version can be only 0, currently
        // witness program is 20-byte RIPEMD160(SHA256(pubkey)) for P2WPKH
        // and 32-byte SHA256(script) for P2WSH
        // https://bitcoin.stackexchange.com/a/71219
        public static string EncodeBech32(string prefix, byte[] publicKey, int limit = 90)
        {
            var words = ConvertBits(publicKey, 8, 5, true);
            if (prefix.Length + 7 + words.Length > limit)
                throw new InvalidOperationException("Exceeds length limit");
            prefix = prefix.ToLowerInvariant();
            // determine chk mod
            var chk = PrefixCheck(prefix);
            var result = prefix + '1';
            for (var i = 0; i < words.Length; ++i)
            {
                var x = words[i];
                if (x >> 5 != 0)
                    throw new InvalidOperationException("Non 5-bit word");
                chk = PolymodStep(chk) ^ x;
                result += CHARSET_BECH32[x];
            }
            for (var i = 0; i < 6; ++i)
            {
                chk = PolymodStep(chk);
            }
            chk ^= 1;
            for (var i = 0; i < 6; ++i)
            {
                var v = (chk >> ((5 - i) * 5)) & 0x1f;
                result += CHARSET_BECH32[v];
            }
            return result;
        }
    }
}
