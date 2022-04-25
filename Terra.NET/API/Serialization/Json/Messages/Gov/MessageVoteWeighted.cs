using Cosmos.SDK.Protos.Gov;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Gov
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageVoteWeighted(ulong ProposalId, [property: JsonPropertyName("voter")] string VoterAddress, WeightedVoteOption[] Options)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "gov/MsgVoteWeighted";
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgVoteWeighted";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Gov.MessageVoteWeighted(
                this.ProposalId,
                this.VoterAddress,
                this.Options.Select(opt => opt.ToModel()).ToArray());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var vote = new MsgVoteWeighted
            {
                ProposalId = this.ProposalId,
                Voter = this.VoterAddress
            };
            vote.Options.AddRange(this.Options.Select(opt => opt.ToProto()));
            return vote;
        }
    }

    internal record WeightedVoteOption(VoteOptionEnum Option, decimal Weight)
    {
        internal Cosmos.SDK.Protos.Gov.WeightedVoteOption ToProto()
        {
            return new Cosmos.SDK.Protos.Gov.WeightedVoteOption
            {
                Option = (VoteOption)this.Option,
                Weight = this.Weight.ToString("D")
            };
        }

        internal NET.Messages.Gov.WeightedVoteOption ToModel()
        {
            return new NET.Messages.Gov.WeightedVoteOption(
                (NET.Messages.Gov.VoteOptionEnum)this.Option,
                this.Weight);
        }
    }
}

