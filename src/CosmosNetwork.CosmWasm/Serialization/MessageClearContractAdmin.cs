using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    [ProtoContract]
    internal record MessageClearContractAdmin(
        [property: ProtoMember(1, Name = "sender"), JsonPropertyName("admin")] string AdminAddress,
        [property: ProtoMember(3, Name = "contract"), JsonPropertyName("contract")] string ContractAddress) : SerializerMessage(CosmWasm.MessageClearContractAdmin.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            return new CosmWasm.MessageClearContractAdmin(this.AdminAddress, this.ContractAddress);
        }
    }
}
