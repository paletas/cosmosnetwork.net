using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    internal class ValidatorCurrentRewards
    {
        public string ValidatorAddress { get; set; }

        public ValidatorReward Rewards { get; set; }
    }

    internal class ValidatorReward
    {
        public int Period { get; set; }

        public DenomAmount[] Rewards { get; set; }
    }
}
