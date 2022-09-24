namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    internal record UnbondingDelegation(string DelegatorAddress, string ValidatorAddress, UnbondingDelegationEntry[] Entries)
    {
        public Staking.UnbondingDelegation ToModel()
        {
            return new Staking.UnbondingDelegation(this.DelegatorAddress, this.ValidatorAddress, this.Entries.Select(e => e.ToModel()).ToArray());
        }
    }

    internal record UnbondingDelegationEntry(ulong CreationHeight, DateTime CompletionTime, decimal InitialBalance, decimal Balance)
    {
        public Staking.UnbondingDelegationEntry ToModel()
        {
            return new Staking.UnbondingDelegationEntry(this.CreationHeight, this.CompletionTime, this.InitialBalance, this.Balance);
        }
    }
}
