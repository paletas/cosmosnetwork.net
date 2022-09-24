namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    internal record StakingPool(ulong BondedTokens, ulong NotBondedTokens)
    {
        public Staking.StakingPool ToModel()
        {
            return new Staking.StakingPool(this.BondedTokens, this.NotBondedTokens);
        }
    }
}
