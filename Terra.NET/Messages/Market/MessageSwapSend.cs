namespace Terra.NET.Messages.Market
{
    public record MessageSwapSend(TerraAddress From, TerraAddress To, string AskDenom, Coin OfferCoin)
        : Message(MessageTypeEnum.MarketSwapSend)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Market.MessageSwapSend(
                this.From.Address,
                this.To.Address,
                this.AskDenom,
                new API.Serialization.Json.DenomAmount(this.OfferCoin.Denom, this.OfferCoin.Amount));
        }
    }
}