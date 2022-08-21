using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Slashing.Serialization
{
    internal record MessageUnjail([property: JsonPropertyName("validator_addr")] string ValidatorAddress) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "slashing/MsgUnjail";

        protected internal override Message ToModel()
        {
            return new Slashing.MessageUnjail(ValidatorAddress);
        }
    }
}

