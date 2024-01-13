using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    internal class DistributionParams
    {
        public decimal BaseProposerReward { get; set; }

        public decimal BonusProposerReward { get; set; }

        public decimal CommunityTax { get; set; }

        [JsonPropertyName("withdraw_addr_enabled")]
        public bool WithdrawAddressEnabled { get; set; }

        public Distribution.DistributionParams ToModel()
        {
            return new Distribution.DistributionParams(
                this.BaseProposerReward,
                this.BonusProposerReward,
                this.CommunityTax,
                this.WithdrawAddressEnabled);
        }
    }
}
