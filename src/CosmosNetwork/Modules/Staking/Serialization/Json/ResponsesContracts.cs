namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    public record GetAddressDelegationsResponse(DelegationBalance[] DelegationResponses, CosmosNetwork.Serialization.Json.Pagination Pagination);

    public record GetAddressDelegationResponse(DelegationBalance DelegationResponse);

    public record GetAddressRedelegationsResponse(RedelegationResponse[] RedelegationResponses, CosmosNetwork.Serialization.Json.Pagination Pagination);

    public record GetAddressUnbondingDelegationsResponse(UnbondingDelegation[] UnbondingResponses, CosmosNetwork.Serialization.Json.Pagination Pagination);

    public record GetAddressUnbondingDelegationResponse(UnbondingDelegation UnbondingResponse);

    public record GetValidatorResponse(Validator Validator);

    public record GetValidatorsResponse(Validator[] Validators, CosmosNetwork.Serialization.Json.Pagination Pagination);

    public record GetDelegationParamsResponse(DelegationParams Params);

    public record GetDelegationPoolResponse(StakingPool Pool);
}
