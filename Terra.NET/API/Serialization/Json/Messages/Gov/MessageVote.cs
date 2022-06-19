using Cosmos.SDK.Protos.Gov;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Gov
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageVote(ulong ProposalId, [property: JsonPropertyName("voter")] string VoterAddress, [property: JsonConverter(typeof(JsonStringEnumConverter))] VoteOptionEnum Option)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "gov/MsgVote";
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgVote";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Gov.MessageVote(this.ProposalId, this.VoterAddress, (NET.Messages.Gov.VoteOptionEnum)this.Option);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgVote
            {
                ProposalId = this.ProposalId,
                Voter = this.VoterAddress,
                Option = (VoteOption)this.Option
            };
        }
    }

    internal enum VoteOptionEnum
    {
        VOTE_OPTION_UNSPECIFIED = 0,
        VOTE_OPTION_YES = 1,
        VOTE_OPTION_ABSTAIN = 2,
        VOTE_OPTION_NO = 3,
        VOTE_OPTION_NO_WITH_VETO = 4
    }
}

