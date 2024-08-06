namespace CosmosNetwork.Modules.Gov
{
    public record MessageDeposit(string MessageType, ulong ProposalId, CosmosAddress Depositor, Coin[] Coins) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1.MsgDeposit";
        public const string COSMOS_BETA_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgDeposit";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageDeposit(
                this.ProposalId,
                this.Depositor.Address,
                this.Coins.Select(coin => coin.ToSerialization()).ToArray());
        }
    }
}