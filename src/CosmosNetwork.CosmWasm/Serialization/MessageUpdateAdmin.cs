using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    [ProtoContract]
    internal record MessageUpdateAdmin(
        [property: ProtoMember(1, Name = "admin"), JsonPropertyName("admin")] string AdminAddress,
        [property: ProtoMember(2, Name = "new_admin"), JsonPropertyName("new_admin")] string NewAdminAddress,
        [property: ProtoMember(3, Name = "contract"), JsonPropertyName("contract")] string ContractAddress) : SerializerMessage(CosmWasm.MessageUpdateAdmin.COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgUpdateAdmin";

        public override Message ToModel()
        {
            return new CosmWasm.MessageUpdateAdmin(this.AdminAddress, this.NewAdminAddress, this.ContractAddress);
        }
    }
}
