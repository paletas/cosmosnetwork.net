namespace CosmosNetwork.Modules.Staking
{
    public record Redelegation(
        CosmosAddress Delegator,
        CosmosAddress SourceValidator,
        CosmosAddress DestinationValidator,
        RedelegationEntry[] Entries)
    {
        internal Serialization.Redelegation ToSerialization()
        {
            return new Serialization.Redelegation
            {
                DelegatorAddress = this.Delegator,
                ValidatorSrcAddress = this.SourceValidator,
                ValidatorDstAddress = this.DestinationValidator,
                Entries = this.Entries.Select(x => x.ToSerialization()).ToArray(),
            };
        }
    }

    public record RedelegationEntry(
        ulong CreationHeight,
        DateTime CompletionTime,
        decimal InitialBalance,
        decimal SharesDestination)
    {
        internal Serialization.RedelegationEntry ToSerialization()
        {
            return new Serialization.RedelegationEntry
            {
                CreationHeight = this.CreationHeight,
                CompletionTime = this.CompletionTime,
                InitialBalance = this.InitialBalance,
                SharesDst = this.SharesDestination,
            };
        }
    }

    public record RedelegationEntryBalance(
        ulong CreationHeight,
        DateTime CompletionTime,
        decimal InitialBalance,
        decimal SharesDestination,
        Coin Balance) 
        : RedelegationEntry(CreationHeight, CompletionTime, InitialBalance, SharesDestination)
    {
        internal new Serialization.Json.RedelegationEntryBalance ToSerialization()
        {
            return new Serialization.Json.RedelegationEntryBalance
            {
                RedelegationEntry = new Serialization.RedelegationEntry
                {
                    CreationHeight = this.CreationHeight,
                    CompletionTime = this.CompletionTime,
                    InitialBalance = this.InitialBalance,
                    SharesDst = this.SharesDestination,
                },
                Balance = this.Balance.ToSerialization(),
            };
        }
    }
}
