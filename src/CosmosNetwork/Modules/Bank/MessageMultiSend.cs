namespace CosmosNetwork.Modules.Bank
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageMultiSend(
        MessageMultiSendInputOutput[] Inputs,
        MessageMultiSendInputOutput[] Outputs) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.bank.v1beta1.MsgMultiSend";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageMultiSend(
                this.Inputs.Select(i => new Serialization.MessageMultiSendInputOutput(
                    i.Address.Address,
                    i.Coins.Select(coin => coin.ToSerialization()).ToArray())
                ).ToArray(),
                this.Outputs.Select(i => new Serialization.MessageMultiSendInputOutput(
                    i.Address.Address,
                    i.Coins.Select(coin => coin.ToSerialization()).ToArray())
                ).ToArray()
            );
        }
    }

    public record MessageMultiSendInputOutput(CosmosAddress Address, Coin[] Coins);
}