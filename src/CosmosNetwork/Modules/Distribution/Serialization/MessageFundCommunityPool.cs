using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    [ProtoContract]
    internal record MessageFundCommunityPool(
        [property: ProtoMember(2, Name = "depositor"), JsonPropertyName("depositor")] string DepositorAddress,
        [property: ProtoMember(1, Name = "amount")] DenomAmount[] Amount) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new Distribution.MessageFundCommunityPool(
                DepositorAddress,
                Amount.Select(amt => amt.ToModel()).ToArray());
        }
    }
}
