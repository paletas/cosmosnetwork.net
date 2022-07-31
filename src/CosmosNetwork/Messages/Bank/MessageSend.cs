namespace CosmosNetwork.Messages.Bank
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageSend(
        CosmosAddress From,
        CosmosAddress To,
        Coin[] Coins) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.bank.v1beta1.MsgSend";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Bank.MessageSend(
                From.Address,
                To.Address,
                Coins.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }
}