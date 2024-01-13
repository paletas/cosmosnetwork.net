using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    [ProtoContract]
    public record MessageFundCommunityPool(
        [property: ProtoMember(2, Name = "depositor"), JsonPropertyName("depositor")] string DepositorAddress,
        [property: ProtoMember(1, Name = "amount")] DenomAmount[] Amount) : SerializerMessage(Distribution.MessageFundCommunityPool.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Distribution.MessageFundCommunityPool(
                this.DepositorAddress,
                this.Amount.Select(amt => amt.ToModel()).ToArray());
        }
    }
}
