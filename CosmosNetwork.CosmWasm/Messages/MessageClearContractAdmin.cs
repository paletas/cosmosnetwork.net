using CosmosNetwork.Serialization;

namespace CosmosNetwork.CosmWasm.Messages
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageClearContractAdmin(
        CosmosAddress Admin,
        CosmosAddress Contract) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgClearContractAdmin";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageClearContractAdmin(Admin, Contract);
        }
    }
}