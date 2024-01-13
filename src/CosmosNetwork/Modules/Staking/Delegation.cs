namespace CosmosNetwork.Modules.Staking
{
    public record Delegation(CosmosAddress Delegator, CosmosAddress Validator, decimal Shares)
    {
        internal Serialization.Delegation ToSerialization()
        {
            return new Serialization.Delegation
            {
                DelegatorAddress = this.Delegator,
                ValidatorAddress = this.Validator,
                Shares = this.Shares
            };
        }
    }
}
