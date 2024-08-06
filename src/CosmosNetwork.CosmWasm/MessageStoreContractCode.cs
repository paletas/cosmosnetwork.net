using CosmosNetwork.Serialization;

namespace CosmosNetwork.CosmWasm
{
    public record MessageStoreContractCode(
        CosmosAddress Sender,
        string WasmByteCode,
        AccessConfig? InstantiatePermission) : Message(COSMOS_DESCRIPTOR)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmwasm.wasm.v1.MsgStoreCode";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageStoreContractCode(
                this.Sender.Address,
                this.WasmByteCode,
                this.InstantiatePermission?.ToSerialization());
        }
    }
}