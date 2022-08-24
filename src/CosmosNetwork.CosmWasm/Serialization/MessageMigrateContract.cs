﻿using CosmosNetwork.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    internal record MessageMigrateContract(
        [property: JsonPropertyName("sender")] string SenderAddress,
        [property: JsonPropertyName("contract")] string ContractAddress,
        ulong CodeId,
        JsonDocument MigrateMsg) : SerializerMessage
    {
        protected override Message ToModel()
        {
            string migrateMessageJson = JsonSerializer.Serialize(MigrateMsg);

            return new CosmWasm.MessageMigrateContract(
                SenderAddress,
                ContractAddress,
                CodeId,
                migrateMessageJson);
        }
    }
}