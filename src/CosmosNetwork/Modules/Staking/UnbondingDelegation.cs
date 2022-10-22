namespace CosmosNetwork.Modules.Staking
{
    public record UnbondingDelegation(CosmosAddress Delegator, CosmosAddress Validator, UnbondingDelegationEntry[] Entries);

    public record UnbondingDelegationEntry(ulong CreationHeight, DateTime CompletionTime, decimal InitialBalance, decimal Balance);
}
