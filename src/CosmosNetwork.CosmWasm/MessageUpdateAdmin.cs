using CosmosNetwork.Serialization;

namespace CosmosNetwork.CosmWasm
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageUpdateAdmin(CosmosAddress Admin, CosmosAddress NewAdmin, CosmosAddress Contract)
        : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmwasm.wasm.v1.MsgUpdateAdmin";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageUpdateAdmin(
                this.Admin.Address,
                this.NewAdmin.Address,
                this.Contract.Address);
        }
    }
}