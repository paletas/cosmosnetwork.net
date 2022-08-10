using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Slashing
{
    internal record MessageUnjail([property: JsonPropertyName("validator_addr")] string ValidatorAddress) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "slashing/MsgUnjail";

        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Slashing.MessageUnjail(ValidatorAddress);
        }
    }
}

