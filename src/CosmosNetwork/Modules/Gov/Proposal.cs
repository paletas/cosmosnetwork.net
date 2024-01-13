using CosmosNetwork.Modules.Gov.Proposals;

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
        internal Serialization.Proposal ToSerialization()
        {
            return new Serialization.Proposal
            {
                Content = this.Content.ToSerialization(),
                DepositEndTime = this.DepositEndTime,
                FinalTallyResult = this.FinalTallyResult.ToSerialization(),
                ProposalId = this.ProposalId,
                Status = (Serialization.ProposalStatusEnum)this.Status,
                SubmitTime = this.SubmitTime,
                TotalDeposit = this.TotalDeposit.Select(x => x.ToSerialization()).ToArray(),
                VotingEndTime = this.VotingEndTime,
                VotingStartTime = this.VotingStartTime
            };
        }
    }
}
