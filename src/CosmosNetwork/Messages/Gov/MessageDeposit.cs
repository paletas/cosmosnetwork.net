namespace CosmosNetwork.Messages.Gov
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageDeposit(ulong ProposalId, CosmosAddress Depositor, Coin[] Coins) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgDeposit";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Gov.MessageDeposit(
                ProposalId,
                Depositor.Address,
                Coins.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }
}