namespace CosmosNetwork.Modules.Distribution
{
    public record DistributionParams(
        decimal BaseProposerReward,
        decimal BonusProposerReward,
        decimal CommunityTax,
        bool WithdrawAddressEnabled)
    {
        internal Serialization.DistributionParams ToSerialization()
        {
            return new Serialization.DistributionParams
            {
                BaseProposerReward = this.BaseProposerReward,
                BonusProposerReward = this.BonusProposerReward,
                CommunityTax = this.CommunityTax,
                WithdrawAddressEnabled = this.WithdrawAddressEnabled,
            };
        }
    }
}
