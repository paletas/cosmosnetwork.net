using CosmosNetwork.Serialization;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm
{
    public record MessageMigrateContractCode(
        CosmosAddress Admin,
        CosmosAddress Contract,
        ulong NewCodeId,
        string MigrateMessage) : Message(COSMOS_DESCRIPTOR)
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