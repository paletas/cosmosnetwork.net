namespace CosmosNetwork.Modules.Staking
{
    public interface IStakingApi
    {
        Task<(DelegationBalance[] Delegations, Pagination Pagination)> GetWalletDelegation(CosmosAddress delegator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default);

        Task<(Redelegation[] Redelegations, Pagination Pagination)> GetWalletRedelegations(CosmosAddress delegator, CosmosAddress? fromValidator = null, CosmosAddress? toValidator = null, PaginationFilter? pagination = null, CancellationToken cancellationToken = default);

        Task<(UnbondingDelegation[] UnbondingDelegations, Pagination Pagination)> GetWalletUnbondingDelegations(CosmosAddress delegator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default);

        Task<(Validator[] Validators, Pagination Pagination)> GetWalletValidators(CosmosAddress delegator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default);

        Task<Validator?> GetWalletValidator(CosmosAddress delegator, CosmosAddress validator, CancellationToken cancellationToken = default);

        Task<(Validator[] Validators, Pagination Pagination)> GetValidators(PaginationFilter? pagination = null, CancellationToken cancellationToken = default);

        Task<Validator?> GetValidator(CosmosAddress validator, CancellationToken cancellationToken = default);

        Task<(DelegationBalance[] Delegations, Pagination Pagination)> GetValidatorDelegation(CosmosAddress validator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default);

        Task<DelegationBalance?> GetValidatorDelegatorBalance(CosmosAddress validator, CosmosAddress delegator, CancellationToken cancellationToken = default);

        Task<(UnbondingDelegation[] UnbondingDelegations, Pagination Pagination)> GetValidatorUnbondingDelegations(CosmosAddress validator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default);

        Task<UnbondingDelegation?> GetValidatorUnbondingDelegator(CosmosAddress validator, CosmosAddress delegator, CancellationToken cancellationToken = default);

        Task<StakingPool> GetStakingDetails(CancellationToken cancellationToken = default);

        Task<DelegationParams> GetStakingParams(CancellationToken cancellationToken = default);
    }
}