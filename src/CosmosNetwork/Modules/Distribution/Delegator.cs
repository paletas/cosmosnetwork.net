namespace CosmosNetwork.Modules.Distribution
{
    public record Delegator(CosmosAddress Address, CosmosAddress ValidatorAddress, ulong Height, int PreviousPeriod, decimal Stake, CosmosAddress? WithdrawAddress)
    {
        internal Serialization.Delegator ToSerialization()
        {
            return new Serialization.Delegator
            {
                DelegatorAddress = this.Address,
                ValidatorAddress = this.ValidatorAddress,
                StartingInfo = new Serialization.DelegatorStartingInfo
                {
                    Height = this.Height,
                    PreviousPeriod = this.PreviousPeriod,
                    Stake = this.Stake
                }
            };
        }

        internal Serialization.DelegatorWithdraw ToWithdrawSerialization()
        {
            if (this.WithdrawAddress is null)
            {
                throw new System.InvalidOperationException("Withdraw address is null");
            }

            return new Serialization.DelegatorWithdraw
            {
                DelegatorAddress = this.Address,
                WithdrawAddress = this.WithdrawAddress
            };
        }
    }
}
