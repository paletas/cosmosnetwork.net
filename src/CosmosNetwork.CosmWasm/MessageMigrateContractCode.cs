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

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageMigrateContractCode(
                Admin.Address,
                Contract.Address,
                NewCodeId,
                JsonDocument.Parse(MigrateMessage));
        }
    }
}