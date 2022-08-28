using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    [ProtoContract]
    internal record MessageStoreContractCode(
        [property: ProtoMember(1, Name = "sender"), JsonPropertyName("sender")] string SenderAddress,
        [property: ProtoMember(2, Name = "wasm_byte_code")] string WasmByteCode,
        [property: ProtoMember(5, Name = "instantiate_permission")] AccessConfig InstantiatePermission) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new CosmWasm.MessageStoreContractCode(
                this.SenderAddress, 
                this.WasmByteCode,
                this.InstantiatePermission.ToModel());
        }
    }
}
