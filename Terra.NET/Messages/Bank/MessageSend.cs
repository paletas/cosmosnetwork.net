namespace Terra.NET.Messages.Bank
{
    public record MessageSend(TerraAddress From, TerraAddress To, Coin[] Coins) : Message(MessageTypeEnum.BankSend)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Bank.MessageSend(
                this.From.Address,
                this.To.Address,
                this.Coins.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }
}