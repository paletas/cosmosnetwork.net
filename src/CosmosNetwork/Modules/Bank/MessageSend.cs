namespace CosmosNetwork.Modules.Bank
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageSend(
        CosmosAddress From,
        CosmosAddress To,
        Coin[] Coins) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.bank.v1beta1.MsgSend";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageSend(
                From.Address,
                To.Address,
                Coins.Select(coin => coin.ToSerialization()).ToArray());
        }
    }
}