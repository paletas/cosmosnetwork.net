namespace CosmosNetwork.Ibc.Applications.Fees
{
    public record MessagePayPacketFee(
        Fee Fee,
        string SourcePortId,
        string SourceChannelId,
        string Signer,
        string[] Relayers) : Message(COSMOS_DESCRIPTOR)
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.applications.fee.v1.MsgPayPacketFee";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
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
