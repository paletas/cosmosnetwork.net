namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    public record StakingPool(ulong BondedTokens, ulong NotBondedTokens)
    {
        public Staking.StakingPool ToModel()
        {
            return new Staking.StakingPool(this.BondedTokens, this.NotBondedTokens);
        }
    }
}
