using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Slashing.Serialization
{
    [ProtoContract]
    public record MessageUnjail(
        [property: ProtoMember(1, Name = "validator_addr"), JsonPropertyName("validator_addr")] string ValidatorAddress) : SerializerMessage(Slashing.MessageUnjail.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Slashing.MessageUnjail(this.ValidatorAddress);
        }
    }
}

