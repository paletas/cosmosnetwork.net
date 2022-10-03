using CosmosNetwork.Ibc.Serialization.Core.Channel;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Applications.Fees
{
    [ProtoContract]
    internal record MessagePayPacketFeeAsync(
        [property: ProtoMember(1, Name = "packet_id")] PacketId PacketId,
        [property: ProtoMember(2, Name = "packet_fee")] PacketFee PacketFee) : SerializerMessage(Ibc.Applications.Fees.MessagePayPacketFeeAsync.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            return new Ibc.Applications.Fees.MessagePayPacketFeeAsync(
                PacketId.ToModel(),
                PacketFee.ToModel());
        }
    }
}
