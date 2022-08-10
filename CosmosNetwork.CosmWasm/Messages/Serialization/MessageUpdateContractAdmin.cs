﻿using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Messages.Serialization
{
    internal record MessageUpdateContractAdmin(
        [property: JsonPropertyName("admin")] string AdminAddress,
        [property: JsonPropertyName("new_admin")] string NewAdminAddress,
        [property: JsonPropertyName("contract")] string ContractAddress) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgUpdateContractAdmin";

        protected override Message ToModel()
        {
            return new Messages.MessageUpdateContractAdmin(AdminAddress, NewAdminAddress, ContractAddress);
        }
    }
}
