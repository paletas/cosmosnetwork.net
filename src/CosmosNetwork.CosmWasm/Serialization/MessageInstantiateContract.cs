using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    [ProtoContract]
    internal record MessageInstantiateContract(
        [property: ProtoMember(1, Name = "sender"), JsonPropertyName("sender")] string SenderAddress,
        [property: ProtoMember(2, Name = "admin"), JsonPropertyName("admin")] string? AdminAddress,
        [property: ProtoMember(3, Name = "code_id")] ulong CodeId,
        [property: ProtoMember(4, Name = "label")] string Label,
        [property: ProtoMember(5, Name = "msg")] JsonDocument Msg,
        [property: ProtoMember(6, Name = "funds")] DenomAmount[] Funds) : SerializerMessage(CosmWasm.MessageInstantiateContract.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            string initMessageJson = JsonSerializer.Serialize(this.Msg);

            return new CosmWasm.MessageInstantiateContract(
                this.SenderAddress,
                string.IsNullOrWhiteSpace(this.AdminAddress) ? null : new CosmosAddress(this.AdminAddress),
                this.CodeId,
                this.Label,
                initMessageJson,
                this.Funds.Select(coin => coin.ToModel()).ToArray());
        }
    }
}
