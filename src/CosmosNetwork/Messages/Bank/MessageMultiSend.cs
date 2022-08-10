namespace CosmosNetwork.Messages.Bank
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageMultiSend(
        MessageMultiSendInputOutput[] Inputs,
        MessageMultiSendInputOutput[] Outputs) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.bank.v1beta1.MsgMultiSend";

        protected internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Bank.MessageMultiSend(
                Inputs.Select(i => new Serialization.Messages.Bank.MessageMultiSendInputOutput(
                    i.Address.Address,
                    i.Coins.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray())
                ).ToArray(),
                Outputs.Select(i => new Serialization.Messages.Bank.MessageMultiSendInputOutput(
                    i.Address.Address,
                    i.Coins.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray())
                ).ToArray()
            );
        }
    }

    public record MessageMultiSendInputOutput(CosmosAddress Address, Coin[] Coins);
}