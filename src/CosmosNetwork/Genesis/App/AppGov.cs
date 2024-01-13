using CosmosNetwork.Modules.Gov;

namespace CosmosNetwork.Genesis.App
{
    public record AppGov(
        GovDepositParams DepositParams,
        Proposal[] Proposals,
        uint StartingProposalId,
        GovTallyParams TallyParams,
        GovVotingParams VotingParams)
    {
        internal Serialization.App.AppGov ToSerialization()
        {
            return new Serialization.App.AppGov
            {
                DepositParams = this.DepositParams.ToSerialization(),
                Proposals = this.Proposals.Select(proposal => proposal.ToSerialization()).ToArray(),
                StartingProposalId = this.StartingProposalId,
                TallyParams = this.TallyParams.ToSerialization(),
                VotingParams = this.VotingParams.ToSerialization()
            };
        }
    }
}
