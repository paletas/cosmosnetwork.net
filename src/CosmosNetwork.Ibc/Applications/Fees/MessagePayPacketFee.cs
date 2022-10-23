namespace CosmosNetwork.Ibc.Applications.Fees
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessagePayPacketFee(
        Fee Fee,
        string SourcePortId,
        string SourceChannelId,
        string Signer,
        string[] Relayers) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.applications.fee.v1.MsgPayPacketFee";

        protected override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.Fees.MessagePayPacketFee(
                this.Fee.ToSerialization(),
                this.SourcePortId,
                this.SourceChannelId,
                this.Signer,
                this.Relayers);
        }
    }
}
