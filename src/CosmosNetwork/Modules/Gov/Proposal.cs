using CosmosNetwork.Modules.Gov.Proposals;
using System.Numerics;

namespace CosmosNetwork.Modules.Gov
{
    public record Proposal(
        ulong ProposalId,
        IProposal Content,
        ProposalStatusEnum Status,
        ProposalTally FinalTallyResult,
        DateTime SubmitTime,
        DateTime DepositEndTime,
        Coin[] TotalDeposit,
        DateTime? VotingStartTime,
        DateTime? VotingEndTime)
    {
    }

    public record ProposalTally(
        BigInteger Yes,
        BigInteger Abstain,
        BigInteger No,
        BigInteger NoWithVeto)
    {

    }
}
