using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using CosmosNetwork.Serialization;
using System.Numerics;

namespace CosmosNetwork.Modules.Gov.Serialization.Json
{
    internal record Proposal(
        ulong ProposalId,
        IProposal Content,
        ProposalStatusEnum Status,
        ProposalTally FinalTallyResult,
        DateTime SubmitTime,
        DateTime DepositEndTime,
        DenomAmount[] TotalDeposit,
        DateTime? VotingStartTime,
        DateTime? VotingEndTime)
    {
        public Gov.Proposal ToModel()
        {
            return new Gov.Proposal(
                this.ProposalId,
                this.Content.ToModel(),
                (Gov.ProposalStatusEnum) this.Status,
                this.FinalTallyResult.ToModel(),
                this.SubmitTime,
                this.DepositEndTime,
                this.TotalDeposit.Select(c => c.ToModel()).ToArray(),
                this.VotingStartTime,
                this.VotingEndTime);
        }
    }

    internal record ProposalTally(
        BigInteger Yes,
        BigInteger Abstain,
        BigInteger No,
        BigInteger NoWithVeto)
    {
        public Gov.ProposalTally ToModel()
        {
            return new Gov.ProposalTally(this.Yes, this.Abstain, this.No, this.NoWithVeto);
        }
    }
}
