using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization
{
    [ProtoContract]
    internal record MessageDeposit(
        [property: ProtoMember(1, Name = "proposal_id")] ulong ProposalId,
        [property: ProtoMember(2, Name = "depositor"), JsonPropertyName("depositor")] string DepositorAddress,
        [property: ProtoMember(3, Name = "amount")] DenomAmount[] Amount) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "gov/MsgDeposit";

        protected internal override Message ToModel()
        {
            return new Gov.MessageDeposit(
                ProposalId,
                DepositorAddress,
                Amount.Select(c => c.ToModel()).ToArray());
        }
    }
}

