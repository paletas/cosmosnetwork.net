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
                this.GasLimit,
                this.Amount?.Select(c => c.ToModel()).ToArray(),
                this.PayerAddress is null ? null : (CosmosAddress)this.PayerAddress,
                this.GranterAddress is null ? null : (CosmosAddress)this.GranterAddress);
        }
    }
}
