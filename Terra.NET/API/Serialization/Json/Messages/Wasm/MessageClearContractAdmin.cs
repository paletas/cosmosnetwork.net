using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.WASM;

namespace Terra.NET.API.Serialization.Json.Messages.Wasm
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageClearContractAdmin([property: JsonPropertyName("admin")] string AdminAddress, [property: JsonPropertyName("contract")] string ContractAddress)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgClearContractAdmin";
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgClearContractAdmin";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Wasm.MessageClearContractAdmin(this.AdminAddress, this.ContractAddress);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgClearContractAdmin
            {
                Admin = this.AdminAddress,
                Contract = this.ContractAddress
            };
        }
    }
}
