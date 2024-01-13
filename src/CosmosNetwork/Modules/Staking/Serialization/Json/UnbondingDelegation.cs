namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    public record UnbondingDelegation(string DelegatorAddress, string ValidatorAddress, UnbondingDelegationEntry[] Entries)
    {
        public Staking.UnbondingDelegation ToModel()
        {
            return new Staking.UnbondingDelegation(this.DelegatorAddress, this.ValidatorAddress, this.Entries.Select(e => e.ToModel()).ToArray());
        }
    }

    public record UnbondingDelegationEntry(ulong CreationHeight, DateTime CompletionTime, decimal InitialBalance, decimal Balance)
    {
        public Staking.UnbondingDelegationEntry ToModel()
        {
            return new Staking.UnbondingDelegationEntry(this.CreationHeight, this.CompletionTime, this.InitialBalance, this.Balance);
        }
    }
}
