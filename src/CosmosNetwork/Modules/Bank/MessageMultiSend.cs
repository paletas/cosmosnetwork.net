namespace CosmosNetwork.Modules.Bank
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageMultiSend(
        MessageMultiSendInputOutput[] Inputs,
        MessageMultiSendInputOutput[] Outputs) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.bank.v1beta1.MsgMultiSend";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageMultiSend(
                Inputs.Select(i => new Serialization.MessageMultiSendInputOutput(
                    i.Address.Address,
                    i.Coins.Select(coin => coin.ToSerialization()).ToArray())
                ).ToArray(),
                Outputs.Select(i => new Serialization.MessageMultiSendInputOutput(
                    i.Address.Address,
                    i.Coins.Select(coin => coin.ToSerialization()).ToArray())
                ).ToArray()
            );
        }
    }

    public record MessageMultiSendInputOutput(CosmosAddress Address, Coin[] Coins);
}