using CosmosNetwork.Ibc.Serialization.Core.Channel;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Applications.Fees
{
    internal record MessagePayPacketFeeAsync(
        PacketId PacketId,
        PacketFee PacketFee) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Ibc.Applications.Fees.MessagePayPacketFeeAsync(
                PacketId.ToModel(),
                PacketFee.ToModel());
        }
    }
}
