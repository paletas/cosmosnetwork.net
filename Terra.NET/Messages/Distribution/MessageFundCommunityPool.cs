namespace Terra.NET.Messages.Distribution
{
    public record MessageFundCommunityPool(TerraAddress Depositor, Coin[] Coins)
        : Message(MessageTypeEnum.DistributionFundCommunityPool)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Distribution.MessageFundCommunityPool(
                this.Depositor.Address,
                this.Coins.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }
}