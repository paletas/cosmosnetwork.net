using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Applications.Fees
{
    internal record MessagePayPacketFee(
        Fee Fee,
        string SourcePortId,
        string SourceChannelId,
        string Signer,
        string[] Relayers) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Ibc.Applications.MessagePayPacketFee(
                Fee.ToModel(),
                SourcePortId,
                SourceChannelId,
                Signer,
                Relayers);
        }
    }
}
