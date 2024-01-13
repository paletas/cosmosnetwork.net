using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization
{
    internal class Proposal
    {
        public ulong ProposalId { get; set; }

        public IProposal Content { get; set; }

        public ProposalStatusEnum Status { get; set; }

        public TallyResult FinalTallyResult { get; set; }

        public DateTime SubmitTime { get; set; }

        public DateTime DepositEndTime { get; set; }

        public DenomAmount[] TotalDeposit { get; set; }

        public DateTime? VotingStartTime { get; set; }

        public DateTime? VotingEndTime { get; set; }

        public Gov.Proposal ToModel()
        {
            return new Gov.Proposal(
                this.ProposalId,
                this.Content.ToModel(),
                (Gov.ProposalStatusEnum)this.Status,
                this.FinalTallyResult.ToModel(),
                this.SubmitTime,
                this.DepositEndTime,
                this.TotalDeposit.Select(c => c.ToModel()).ToArray(),
                this.VotingStartTime,
                this.VotingEndTime);
        }
    }
}
