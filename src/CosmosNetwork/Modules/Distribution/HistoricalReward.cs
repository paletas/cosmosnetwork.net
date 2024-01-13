namespace CosmosNetwork.Modules.Distribution
{
    public record HistoricalReward(int Period, CoinDecimal[] CumulativeRewardRatio)
    {
        internal Serialization.HistoricalReward ToSerialization()
        {
            return new Serialization.HistoricalReward
            {
                CumulativeRewardRatio = this.CumulativeRewardRatio.Select(c => c.ToSerialization()).ToArray(),
                ReferenceCount = this.CumulativeRewardRatio.Length,
            };
        }
    }
}
