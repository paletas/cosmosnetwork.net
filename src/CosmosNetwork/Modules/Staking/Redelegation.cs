namespace CosmosNetwork.Modules.Staking
{
    public record Redelegation(
        CosmosAddress Delegator,
        CosmosAddress SourceValidator,
        CosmosAddress DestinationValidator,
        RedelegationEntry[] Entries);

    public record RedelegationEntry(
        ulong CreationHeight,
        DateTime CompletionTime,
        decimal InitialBalance,
        decimal SharesDestination,
        Coin Balance);
}
