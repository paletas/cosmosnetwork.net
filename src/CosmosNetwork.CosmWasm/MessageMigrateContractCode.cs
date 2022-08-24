using CosmosNetwork.Serialization;

namespace CosmosNetwork.CosmWasm
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageMigrateContractCode(
        CosmosAddress Sender,
        ulong CodeId,
        string WasmByteCode) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgMigrateCode";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageMigrateContractCode(Sender.Address, CodeId, WasmByteCode);
        }
    }
}