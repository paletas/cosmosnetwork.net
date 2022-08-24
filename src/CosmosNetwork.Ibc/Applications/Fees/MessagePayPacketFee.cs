namespace CosmosNetwork.Ibc.Applications.Fees
{
    public record MessagePayPacketFee(
        Fee Fee,
        string SourcePortId,
        string SourceChannelId,
        string Signer,
        string[] Relayers) : Message
    {
        protected override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.MessagePayPacketFee(
                Fee.ToSerialization(),
                SourcePortId,
                SourceChannelId,
                Signer,
                Relayers);
        }
    }
}
