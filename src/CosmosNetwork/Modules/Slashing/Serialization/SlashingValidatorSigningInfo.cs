using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Slashing.Serialization
{
    public class SlashingValidatorSigningInfo
    {
        public string Address { get; set; }

        [JsonPropertyName("validator_signing_info")]
        public SigningInfo SigningInfo { get; set; }
    }
}
