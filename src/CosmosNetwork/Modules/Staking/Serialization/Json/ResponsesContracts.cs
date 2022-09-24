namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    internal record GetAddressDelegationsResponse(DelegationBalance[] DelegationResponses, CosmosNetwork.Serialization.Json.Pagination Pagination);

    internal record GetAddressDelegationResponse(DelegationBalance DelegationResponse);

    internal record GetAddressRedelegationsResponse(RedelegationResponse[] RedelegationResponses, CosmosNetwork.Serialization.Json.Pagination Pagination);

    internal record GetAddressUnbondingDelegationsResponse(UnbondingDelegation[] UnbondingResponses, CosmosNetwork.Serialization.Json.Pagination Pagination);

    internal record GetAddressUnbondingDelegationResponse(UnbondingDelegation UnbondingResponse);

    internal record GetValidatorResponse(Validator Validator);

    internal record GetValidatorsResponse(Validator[] Validators, CosmosNetwork.Serialization.Json.Pagination Pagination);

    internal record GetDelegationParamsResponse(DelegationParams Params);

    internal record GetDelegationPoolResponse(StakingPool Pool);
}
