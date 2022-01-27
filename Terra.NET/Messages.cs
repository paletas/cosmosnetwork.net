namespace Terra.NET
{
    public abstract record Message()
    {
        internal abstract API.Serialization.Json.Message ToJson();
    };

    public record MessageSend(TerraAddress FromAddress, TerraAddress ToAddress, Coin[] Coins) : Message()
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.MessageSend(this.FromAddress.Address, this.ToAddress.Address, this.Coins.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }

    public record MessageExecuteContract<T>(Coin[] Coins, TerraAddress Sender, TerraAddress Contract, T ExecuteMessage) : Message()
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.MessageExecuteContract<T>(
                this.Coins.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                this.Sender.Address,
                this.Contract.Address,
                this.ExecuteMessage
            );
        }
    }

    public record MessageSwap(TerraAddress Trader, string AskDenom, Coin OfferCoin) : Message()
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.MessageSwap(this.Trader.Address, this.AskDenom, new API.Serialization.Json.DenomAmount(this.OfferCoin.Denom, this.OfferCoin.Amount));
        }
    }
}
