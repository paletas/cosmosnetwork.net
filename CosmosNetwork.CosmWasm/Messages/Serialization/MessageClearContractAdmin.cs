using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Messages.Serialization
{
    internal record MessageClearContractAdmin(
        [property: JsonPropertyName("admin")] string AdminAddress,
        [property: JsonPropertyName("contract")] string ContractAddress) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Messages.MessageClearContractAdmin(AdminAddress, ContractAddress);
        }
    }
}
