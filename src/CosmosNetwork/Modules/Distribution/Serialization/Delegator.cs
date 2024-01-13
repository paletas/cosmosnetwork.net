namespace CosmosNetwork.Modules.Distribution.Serialization
{
    internal class Delegator
    {
        public string DelegatorAddress { get; set; }

        public string ValidatorAddress { get; set; }

        public DelegatorStartingInfo StartingInfo { get; set; }

        public Distribution.Delegator ToModel(DelegatorWithdraw? delegatorWithdraw)
        {
            return new Distribution.Delegator(
                this.DelegatorAddress,
                this.ValidatorAddress,
                this.StartingInfo.Height,
                this.StartingInfo.PreviousPeriod,
                this.StartingInfo.Stake,
                delegatorWithdraw is null ? (CosmosAddress?)null : delegatorWithdraw.WithdrawAddress);
        }
    }
}
