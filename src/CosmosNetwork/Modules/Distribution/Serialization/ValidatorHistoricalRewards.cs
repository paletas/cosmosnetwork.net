using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    internal class ValidatorHistoricalRewards
    {
        public string ValidatorAddress { get; set; }

        public int Period { get; set; }

        public HistoricalReward Rewards { get; set; }

        public Distribution.HistoricalReward ToModel()
        {
            return new Distribution.HistoricalReward(this.Period, this.Rewards.CumulativeRewardRatio.Select(c => c.ToDecimalModel()).ToArray());
        }
    }

    internal class HistoricalReward
    {
        public DenomAmount[] CumulativeRewardRatio { get; set; }

        public int ReferenceCount { get; set; }
    }
}
