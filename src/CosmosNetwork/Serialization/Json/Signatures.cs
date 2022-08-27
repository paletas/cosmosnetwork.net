using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json
{
    internal class SignatureDescriptor
    {
        [JsonConverter(typeof(PublicKeyConverter))]
        public PublicKey PublicKey { get; set; } = null!;

        [JsonPropertyName("mode_info")]
        public SignatureData Data { get; set; } = null!;

        public ulong Sequence { get; set; }
    }
}
