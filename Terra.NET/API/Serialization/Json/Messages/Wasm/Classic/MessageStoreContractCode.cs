using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.WASM;

namespace Terra.NET.API.Serialization.Json.Messages.Wasm.Classic
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageStoreContractCode([property: JsonPropertyName("sender")] string SenderAddress, string WasmByteCode)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgStoreCode";
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgStoreCode";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Wasm.MessageStoreContractCode(this.SenderAddress, this.WasmByteCode);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgStoreCode
            {
                Sender = this.SenderAddress,
                WasmByteCode = ByteString.FromBase64(this.WasmByteCode)
            };
        }
    }
}
