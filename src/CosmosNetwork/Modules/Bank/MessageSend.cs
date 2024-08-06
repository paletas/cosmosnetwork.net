namespace CosmosNetwork.Modules.Bank
{
    public record MessageSend(
        string MessageType,
        CosmosAddress From,
        CosmosAddress To,
        Coin[] Coins) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.bank.v1beta1.MsgSend";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageSend(
                this.From.Address,
                this.To.Address,
                this.Coins.Select(coin => coin.ToSerialization()).ToArray());
        }
    }
}