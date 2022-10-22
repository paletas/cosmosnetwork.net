using ProtoBuf;

namespace CosmosNetwork.Serialization
{
    [ProtoContract]
    internal record Fee(
        [property: ProtoMember(1, Name = "amount")] DenomAmount[]? Amount = null,
        [property: ProtoMember(2, Name = "gas_limit")] ulong? GasLimit = null,
        [property: ProtoMember(3, Name = "payer")] string? PayerAddress = null,
        [property: ProtoMember(4, Name = "granter")] string? GranterAddress = null)
    {
        public CosmosNetwork.Fee ToModel()
        {
            return new CosmosNetwork.Fee(
                GasLimit,
                Amount?.Select(c => c.ToModel()).ToArray(),
                PayerAddress is null ? null : (CosmosAddress)PayerAddress,
                GranterAddress is null ? null : (CosmosAddress)GranterAddress);
        }
    }
}
