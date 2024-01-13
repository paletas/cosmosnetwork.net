namespace CosmosNetwork.Modules.Distribution
{
    public record Validator(
        CosmosAddress Address,
        CoinDecimal[] AccumulatedCommissions,
        int RewardPeriod,
        CoinDecimal[] CurrentRewards,
        HistoricalReward[] HistoricalRewards,
        CoinDecimal[] OutstandingRewards,
        SlashingEvent[] SlashingEvents)
    {
        internal (
            Serialization.ValidatorCommissions Commissions, 
            Serialization.ValidatorCurrentRewards currentRewards,
            Serialization.ValidatorHistoricalRewards[] historicalRewards,
            Serialization.ValidatorOutstandingRewards OutstandingRewards, 
            Serialization.ValidatorSlashEvent[] SlashEvents) ToSerialization()
        {
            Serialization.ValidatorCommissions validatorCommissions = new Serialization.ValidatorCommissions
            {
                ValidatorAddress = this.Address,
                Accumulated = new Serialization.ValidatorAccumulated
                {
                    Commission = this.AccumulatedCommissions.Select(c => c.ToSerialization()).ToArray(),
                }
            };

            Serialization.ValidatorCurrentRewards validatorCurrentRewards = new Serialization.ValidatorCurrentRewards
            {
                ValidatorAddress = this.Address,
                Rewards = new Serialization.ValidatorReward
                {
                    Rewards = this.CurrentRewards.Select(c => c.ToSerialization()).ToArray(),
                    Period = this.RewardPeriod
                }
            };

            Serialization.ValidatorHistoricalRewards[] validatorHistoricalRewards = this.HistoricalRewards.Select(hr => new Serialization.ValidatorHistoricalRewards
            {
                ValidatorAddress = this.Address,
                Period = hr.Period,
                Rewards = hr.ToSerialization(),
            }).ToArray();

            Serialization.ValidatorOutstandingRewards validatorOutstandingRewards = new Serialization.ValidatorOutstandingRewards
            {
                ValidatorAddress = this.Address,
                OutstandingRewards = this.OutstandingRewards.Select(c => c.ToSerialization()).ToArray(),
            };

            Serialization.ValidatorSlashEvent[] validatorSlashEvents = this.SlashingEvents.Select(se => se.ToSerialization()).ToArray();

            return (validatorCommissions, validatorCurrentRewards, validatorHistoricalRewards, validatorOutstandingRewards, validatorSlashEvents);
        }
    }
}
