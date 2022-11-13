using CosmosNetwork.Serialization;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageMigrateContractCode(
        CosmosAddress Admin,
        CosmosAddress Contract,
        ulong NewCodeId,
        string MigrateMessage) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmwasm.wasm.v1.MsgMigrateContract";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageMigrateContractCode(
                this.Admin.Address,
                this.Contract.Address,
                this.NewCodeId,
                JsonDocument.Parse(this.MigrateMessage));
        }
    }
}