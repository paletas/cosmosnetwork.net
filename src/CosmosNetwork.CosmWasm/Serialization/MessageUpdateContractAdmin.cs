using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    [ProtoContract]
    internal record MessageUpdateContractAdmin(
        [property: ProtoMember(1, Name = "admin"), JsonPropertyName("admin")] string AdminAddress,
        [property: ProtoMember(2, Name = "new_admin"), JsonPropertyName("new_admin")] string NewAdminAddress,
        [property: ProtoMember(3, Name = "contract"), JsonPropertyName("contract")] string ContractAddress) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgUpdateContractAdmin";

        protected override Message ToModel()
        {
            return new CosmWasm.MessageUpdateContractAdmin(AdminAddress, NewAdminAddress, ContractAddress);
        }
    }
}
