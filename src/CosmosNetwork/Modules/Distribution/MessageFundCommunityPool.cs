namespace CosmosNetwork.Modules.Distribution
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageFundCommunityPool(
        CosmosAddress Depositor,
        Coin[] Coins) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgFundCommunityPool";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageFundCommunityPool(
                this.Depositor.Address,
                this.Coins.Select(coin => coin.ToSerialization()).ToArray());
        }
    }
}