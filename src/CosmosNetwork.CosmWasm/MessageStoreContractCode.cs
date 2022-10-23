using CosmosNetwork.Serialization;

namespace CosmosNetwork.CosmWasm
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageStoreContractCode(
        CosmosAddress Sender,
        string WasmByteCode,
        AccessConfig? InstantiatePermission) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmwasm.wasm.v1.MsgStoreCode";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageStoreContractCode(
                this.Sender.Address,
                this.WasmByteCode,
                this.InstantiatePermission?.ToSerialization());
        }
    }
}