namespace CosmosNetwork.Modules.Distribution
{
    public record MessageFundCommunityPool(
        string MessageType,
        CosmosAddress Depositor,
        Coin[] Coins) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1.MsgFundCommunityPool";
        public const string COSMOS_BETA_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgFundCommunityPool";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageFundCommunityPool(
                this.Depositor.Address,
                this.Coins.Select(coin => coin.ToSerialization()).ToArray());
        }
    }
}