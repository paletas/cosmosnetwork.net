using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    [ProtoContract]
    internal record MessageMigrateContractCode(
        [property: ProtoMember(1, Name = "sender"), JsonPropertyName("sender")] string SenderAddress,
        [property: ProtoMember(2, Name = "contract"), JsonPropertyName("contract")] string ContractAddress,
        [property: ProtoMember(3, Name = "code_id")] ulong CodeId,
        [property: ProtoMember(4, Name = "msg")] JsonDocument MigrateMsg) : SerializerMessage(CosmWasm.MessageMigrateContractCode.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            string migrateMessageJson = JsonSerializer.Serialize(MigrateMsg);

            return new CosmWasm.MessageMigrateContractCode(
                SenderAddress,
                ContractAddress,
                CodeId,
                migrateMessageJson);
        }
    }
}
