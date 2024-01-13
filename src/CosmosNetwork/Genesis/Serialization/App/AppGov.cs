using CosmosNetwork.Modules.Gov.Serialization;

namespace CosmosNetwork.Genesis.Serialization.App
{
    internal class AppGov
    {
        public GovDepositParams DepositParams { get; set; }

        public Proposal[] Proposals { get; set; }

        public uint StartingProposalId { get; set; }

        public GovTallyParams TallyParams { get; set; }

        public GovVotingParams VotingParams { get; set; }

        public Genesis.App.AppGov ToModel()
        {
            return new Genesis.App.AppGov(
                this.DepositParams.ToModel(),
                this.Proposals.Select(proposal => proposal.ToModel()).ToArray(),
                this.StartingProposalId,
                this.TallyParams.ToModel(),
                this.VotingParams.ToModel());
        }
    }
}
