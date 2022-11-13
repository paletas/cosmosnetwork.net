using Cryptography.ECDSA;

namespace CosmosNetwork.Keys
{
    internal class MultisigPublicKey : IPublicKey
    {
        private readonly uint _threshold;
        private readonly IPublicKey[] _keys;

        public MultisigPublicKey(uint threshold, IPublicKey[] keys)
        {
            this._threshold = threshold;
            this._keys = keys;

            this.RawKey = GetAddress(threshold, keys);
        }

        public byte[] RawKey { get; init; }

        public string AsAddress(string prefix) => Bech32.Encode(prefix, this.RawKey);

        public Serialization.Proto.PublicKey ToProto()
        {
            return new Serialization.Proto.MultisigKey(this._threshold, this._keys.Select(k => k.ToProto()).ToArray());
        }

        public Serialization.Json.PublicKey ToJson()
        {
            return new Serialization.Json.MultisigKey(this._threshold, this._keys.Select(k => k.ToJson()).ToArray());
        }

        private static byte[] GetAddress(uint threshold, IKey[] keys)
        {
            string innerPrefix = $"cosmos.crypto.multisig.PubKey/{threshold}";
            return Composed(innerPrefix, keys);
        }

        private static byte[] Composed(string prefix, IEnumerable<IKey> keys)
        {
            var addresses = keys.Select(k => PrependAddress(k.RawKey))
                .OrderBy(key => key, new ByteArrayComparer())
                .ToArray();

            return Sha256Manager.GetHash(Concatenate(addresses));
        }

        private static byte[] PrependAddress(ReadOnlySpan<byte> address)
        {
            byte[] pAddress = new byte[address.Length + 1];
            pAddress[0] = (byte)(address.Length * 8);
            address.CopyTo(pAddress.AsSpan()[1..]);
            return pAddress;
        }

        private static byte[] Concatenate(params byte[][] arrays)
        {
            byte[] concat = new byte[arrays.Sum(arr => arr.Length)];
            int currIndex = 0;
            foreach (var arr in arrays)
            {
                Array.Copy(arr, 0, concat, currIndex, arr.Length);
                currIndex += arr.Length;
            }
            return concat;
        }

        public class ByteArrayComparer : IComparer<byte[]>
        {
            public int Compare(byte[]? x, byte[]? y)
            {
                if (x is null && y is null) return default;
                else if (x is null) return 1;
                else if (y is null) return -1;

                int result;
                for (int index = 0; index < Math.Min(x.Length, y.Length); index++)
                {
                    result = x[index].CompareTo(y[index]);
                    if (result != 0) return result;
                }
                return x.Length.CompareTo(y.Length);
            }
        }
    }
}
