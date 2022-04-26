using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.WASM;

namespace Terra.NET.API.Serialization.Json.Messages.Wasm
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageMigrateCode([property: JsonPropertyName("sender")] string SenderAddress, ulong CodeId, string WasmByteCode)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgMigrateCode";
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgMigrateCode";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Wasm.MessageMigrateCode(this.SenderAddress, this.CodeId, this.WasmByteCode);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgMigrateCode
            {
                Sender = this.SenderAddress,
                CodeId = this.CodeId,
                WasmByteCode = ByteString.FromBase64(this.WasmByteCode)
            };
        }
    }
}
