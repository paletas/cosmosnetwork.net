using Cosmos.SDK.Protos.Crypto.Keys.Secp256k1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Text;

namespace Terra.NET.API.Serialization.Protos.Mappers
{
    internal static class PublicKeyMappers
    {
        public static PubKey ToProto(this PublicKey publicKey)
        {
            var base64Key = Convert.ToBase64String(publicKey.RawKey);
            return new PubKey
            {
                Key = ByteString.CopyFrom(base64Key, Encoding.UTF8)
            };
        }

        public static Any PackAny(this PublicKey publicKey)
        {
            return Any.Pack(publicKey.ToProto(), string.Empty);
        }
    }
}
