using Google.Protobuf;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.WASM;

namespace Terra.NET.API.Serialization.Json.Messages.Wasm
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageMigrateContract([property: JsonPropertyName("admin")] string AdminAddress, [property: JsonPropertyName("contract")] string ContractAddress, ulong NewCodeId, JsonDocument MigrateMsg)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgMigrateContract";
        public const string COSMOS_DESCRIPTOR = "/cosmos.wasm.v1beta1.MsgMigrateContract";

        internal override NET.Message ToModel()
        {
            string migrateMessageJson = JsonSerializer.Serialize(this.MigrateMsg);

            return new NET.Messages.Wasm.MessageMigrateContract(
                this.AdminAddress,
                this.ContractAddress,
                this.NewCodeId,
                migrateMessageJson);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            string migrateMessageJson = JsonSerializer.Serialize(this.MigrateMsg, serializerOptions);

            return new MsgMigrateContract
            {
                Admin = this.AdminAddress,
                Contract = this.ContractAddress,
                NewCodeId = this.NewCodeId,
                MigrateMsg = ByteString.CopyFrom(migrateMessageJson, Encoding.UTF8)
            };
        }
    }
}
