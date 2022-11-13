namespace CosmosNetwork.Modules.Gov
{
    public interface IGovApi
    {
        Task<GovParams> GetGovernanceParams(CancellationToken cancellationToken = default);

        Task<GovVotingParams> GetGovernanceVotingParams(CancellationToken cancellationToken = default);

        Task<GovDepositParams> GetGovernanceDepositParams(CancellationToken cancellationToken = default);

        Task<GovTallyParams> GetGovernanceTallyParams(CancellationToken cancellationToken = default);

        Task<Proposal?> GetProposal(ulong id, CancellationToken cancellationToken = default);
    }
}
