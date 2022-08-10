using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Distribution
{
    internal record MessageFundCommunityPool(
        [property: JsonPropertyName("depositor")] string DepositorAddress,
        DenomAmount[] Amount) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Distribution.MessageFundCommunityPool(
                DepositorAddress,
                Amount.Select(amt => amt.ToModel()).ToArray());
        }
    }
}
