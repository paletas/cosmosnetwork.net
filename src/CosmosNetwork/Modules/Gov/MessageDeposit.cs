namespace CosmosNetwork.Modules.Gov
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageDeposit(ulong ProposalId, CosmosAddress Depositor, Coin[] Coins) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgDeposit";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageDeposit(
                ProposalId,
                Depositor.Address,
                Coins.Select(coin => new CosmosNetwork.Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }
}