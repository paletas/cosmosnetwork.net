using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.WASM;

namespace Terra.NET.API.Serialization.Json.Messages.Wasm
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageUpdateContractAdmin([property: JsonPropertyName("admin")] string AdminAddress, [property: JsonPropertyName("new_admin")] string NewAdminAddress, [property: JsonPropertyName("contract")] string ContractAddress)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgUpdateContractAdmin";
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgUpdateContractAdmin";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Wasm.MessageUpdateContractAdmin(this.AdminAddress, this.NewAdminAddress, this.ContractAddress);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgUpdateContractAdmin
            {
                Admin = this.AdminAddress,
                NewAdmin = this.NewAdminAddress,
                Contract = this.ContractAddress
            };
        }
    }
}
