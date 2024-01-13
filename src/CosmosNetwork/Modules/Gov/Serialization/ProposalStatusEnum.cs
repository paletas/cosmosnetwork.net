using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ProposalStatusEnum
    {
        [EnumMember(Value = "PROPOSAL_STATUS_UNSPECIFIED")]
        Unspecified = 0,

        [EnumMember(Value = "PROPOSAL_STATUS_DEPOSIT_PERIOD")]
        DepositPeriod = 1,

        [EnumMember(Value = "PROPOSAL_STATUS_VOTING_PERIOD")]
        VotingPeriod = 2,

        [EnumMember(Value = "PROPOSAL_STATUS_PASSED")]
        Passed = 3,

        [EnumMember(Value = "PROPOSAL_STATUS_REJECTED")]
        Rejected = 4,

        [EnumMember(Value = "PROPOSAL_STATUS_FAILED")]
        Failed = 5
    }
}
