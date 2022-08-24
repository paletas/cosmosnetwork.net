using CosmosNetwork.Ibc.Core.Channel;

namespace CosmosNetwork.Ibc.Applications.Fees
{
    public record MessagePayPacketFeeAsync(
        PacketId PacketId,
        PacketFee PacketFee) : Message
    {
        protected override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.Fees.MessagePayPacketFeeAsync(
                PacketId.ToSerialization(),
                PacketFee.ToSerialization());
        }
    }
}
