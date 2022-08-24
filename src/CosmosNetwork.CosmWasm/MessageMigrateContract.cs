﻿using CosmosNetwork.Serialization;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageMigrateContract(
        CosmosAddress Admin,
        CosmosAddress Contract,
        ulong NewCodeId,
        string MigrateMessage) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmwasm.wasm.v1.MsgMigrateContract";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageMigrateContract(
                Admin.Address,
                Contract.Address,
                NewCodeId,
                JsonDocument.Parse(MigrateMessage));
        }
    }
}