using CosmosNetwork.Serialization;

namespace CosmosNetwork.CosmWasm
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageUpdateContractAdmin(CosmosAddress Admin, CosmosAddress NewAdmin, CosmosAddress Contract)
        : Message
    {
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgUpdateContractAdmin";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageUpdateContractAdmin(
                Admin.Address,
                NewAdmin.Address,
                Contract.Address);
        }
    }
}