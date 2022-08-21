using CosmosNetwork.Serialization;

namespace CosmosNetwork.CosmWasm.Messages
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageStoreContractCode(CosmosAddress Sender, string WasmByteCode)
        : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmwasm.wasm.v1.MsgStoreCode";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageStoreContractCode(Sender.Address, WasmByteCode);
        }
    }
}