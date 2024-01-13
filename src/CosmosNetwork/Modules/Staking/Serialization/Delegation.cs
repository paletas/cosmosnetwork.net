namespace CosmosNetwork.Modules.Staking.Serialization
{
    public class Delegation
    {
        public string DelegatorAddress { get; set; }

        public string ValidatorAddress { get; set; }

        public decimal Shares { get; set; }

        public Staking.Delegation ToModel()
        {
            return new Staking.Delegation(this.DelegatorAddress, this.ValidatorAddress, this.Shares);
        }
    }
}
