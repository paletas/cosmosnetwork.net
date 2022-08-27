using CosmosNetwork.Ibc.Core.Channel;

namespace CosmosNetwork.Ibc.Applications.Fees
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessagePayPacketFeeAsync(
        PacketId PacketId,
        PacketFee PacketFee) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.applications.fee.v1.MsgPayPacketFeeAsync";

        protected override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.Fees.MessagePayPacketFeeAsync(
                PacketId.ToSerialization(),
                PacketFee.ToSerialization());
        }
    }
}
