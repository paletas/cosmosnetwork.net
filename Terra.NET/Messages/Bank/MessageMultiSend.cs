namespace Terra.NET.Messages.Bank
{
    public record MessageMultiSend(MessageMultiSendInputOutput[] Inputs, MessageMultiSendInputOutput[] Outputs) : Message(MessageTypeEnum.BankMultiSend)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Bank.MessageMultiSend(
                this.Inputs.Select(i => new API.Serialization.Json.Messages.Bank.MessageMultiSendInputOutput(i.Address.Address, i.Coins.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray())).ToArray(),
                this.Outputs.Select(i => new API.Serialization.Json.Messages.Bank.MessageMultiSendInputOutput(i.Address.Address, i.Coins.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray())).ToArray()
            );
        }
    }

    public record MessageMultiSendInputOutput(TerraAddress Address, Coin[] Coins);
}