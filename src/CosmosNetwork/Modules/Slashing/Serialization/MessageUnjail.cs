using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Slashing.Serialization
{
    [ProtoContract]
    internal record MessageUnjail(
        [property: ProtoMember(1, Name = "validator_addr"), JsonPropertyName("validator_addr")] string ValidatorAddress) : SerializerMessage(Slashing.MessageUnjail.COSMOS_DESCRIPTOR)
    {
        protected internal override Message ToModel()
        {
            return new Slashing.MessageUnjail(ValidatorAddress);
        }
    }
}

