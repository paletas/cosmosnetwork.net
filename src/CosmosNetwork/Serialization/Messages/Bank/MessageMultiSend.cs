namespace CosmosNetwork.Serialization.Messages.Bank
{
    internal record MessageMultiSend(
        MessageMultiSendInputOutput[] Inputs,
        MessageMultiSendInputOutput[] Outputs) : SerializerMessage
    {
        internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Bank.MessageMultiSend(
                Inputs.Select(i => i.ToModel()).ToArray(),
                Outputs.Select(o => o.ToModel()).ToArray());
        }
    }

    internal record MessageMultiSendInputOutput(string Address, DenomAmount[] Coins)
    {
        internal CosmosNetwork.Messages.Bank.MessageMultiSendInputOutput ToModel()
        {
            return new CosmosNetwork.Messages.Bank.MessageMultiSendInputOutput(
                Address,
                Coins.Select(c => c.ToModel()).ToArray());
        }
    }
}
