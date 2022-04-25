namespace Terra.NET.Messages.Market
{
    public record MessageSwap(TerraAddress Trader, string AskDenom, Coin OfferCoin) : Message(MessageTypeEnum.MarketSwap)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Market.MessageSwap(this.Trader.Address, this.AskDenom, new API.Serialization.Json.DenomAmount(this.OfferCoin.Denom, this.OfferCoin.Amount));
        }
    }
}