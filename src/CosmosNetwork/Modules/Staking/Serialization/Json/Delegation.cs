using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    internal record Delegation(string DelegatorAddress, string ValidatorAddress, decimal Shares)
    {
        public Staking.Delegation ToModel()
        {
            return new Staking.Delegation(this.DelegatorAddress, this.ValidatorAddress, this.Shares);
        }
    }

    internal record DelegationBalance(Delegation Delegation, DenomAmount Balance)
    {
        public Staking.DelegationBalance ToModel()
        {
            return new Staking.DelegationBalance(this.Delegation.ToModel(), this.Balance.ToModel());
        }
    }
}
