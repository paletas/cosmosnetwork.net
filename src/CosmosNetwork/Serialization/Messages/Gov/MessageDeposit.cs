using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Gov
{
    internal record MessageDeposit(
        ulong ProposalId,
        [property: JsonPropertyName("depositor")] string DepositorAddress,
        DenomAmount[] Amount) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "gov/MsgDeposit";

        internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Gov.MessageDeposit(
                ProposalId,
                DepositorAddress,
                Amount.Select(c => c.ToModel()).ToArray());
        }
    }
}

