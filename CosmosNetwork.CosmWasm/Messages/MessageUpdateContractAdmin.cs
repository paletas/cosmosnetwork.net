using CosmosNetwork.Serialization;

namespace CosmosNetwork.CosmWasm.Messages
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageUpdateContractAdmin(CosmosAddress Admin, CosmosAddress NewAdmin, CosmosAddress Contract)
        : Message
    {
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgUpdateContractAdmin";

        protected override SerializerMessage ToJson()
        {
            return new Serialization.MessageUpdateContractAdmin(
                Admin.Address,
                NewAdmin.Address,
                Contract.Address);
        }
    }
}