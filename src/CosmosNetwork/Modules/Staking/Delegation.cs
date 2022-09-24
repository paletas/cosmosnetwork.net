namespace CosmosNetwork.Modules.Staking
{
    public record Delegation(CosmosAddress Delegator, CosmosAddress Validator, decimal Shares);

    public record DelegationBalance(Delegation Delegation, Coin Balance);
}
