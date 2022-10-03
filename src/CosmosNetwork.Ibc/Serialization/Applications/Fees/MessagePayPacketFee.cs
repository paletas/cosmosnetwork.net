using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Applications.Fees
{
    [ProtoContract]
    internal record MessagePayPacketFee(
        [property: ProtoMember(1, Name = "fee")] Fee Fee,
        [property: ProtoMember(2, Name = "source_port_id")] string SourcePortId,
        [property: ProtoMember(3, Name = "source_channel_id")] string SourceChannelId,
        [property: ProtoMember(4, Name = "signer")] string Signer,
        [property: ProtoMember(5, Name = "relayers")] string[] Relayers) : SerializerMessage(Ibc.Applications.Fees.MessagePayPacketFee.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            return new Ibc.Applications.Fees.MessagePayPacketFee(
                Fee.ToModel(),
                SourcePortId,
                SourceChannelId,
                Signer,
                Relayers);
        }
    }
}
