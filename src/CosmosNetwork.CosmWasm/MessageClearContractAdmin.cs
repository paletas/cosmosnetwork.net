using CosmosNetwork.Serialization;

namespace CosmosNetwork.CosmWasm
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageClearContractAdmin(
        CosmosAddress Admin,
        CosmosAddress Contract) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgClearContractAdmin";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageClearContractAdmin(this.Admin, this.Contract);
        }
    }
}