namespace CosmosNetwork.Messages.Distribution
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageFundCommunityPool(
        CosmosAddress Depositor,
        Coin[] Coins) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgFundCommunityPool";

        protected internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Distribution.MessageFundCommunityPool(
                Depositor.Address,
                Coins.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }
}