using Cosmos.SDK.Protos.Gov;
using Google.Protobuf;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Gov
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageVote(ulong ProposalId, [property: JsonPropertyName("voter")] string VoterAddress, VoteOptionEnum Option)
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
        [EnumMember(Value = "OptionEmpty")]
        Unspecified = 0,

        [EnumMember(Value = "OptionYes")]
        Yes = 1,

        [EnumMember(Value = "OptionAbstain")]
        Abstain = 2,

        [EnumMember(Value = "OptionNo")]
        No = 3,

        [EnumMember(Value = "OptionNoWithVeto")]
        NoWithVeto = 4
    }
}

