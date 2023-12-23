using CosmosNetwork.Ibc.Core.Channel;

namespace CosmosNetwork.Ibc.Applications.Fees
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessagePayPacketFeeAsync(
        PacketId PacketId,
        PacketFee PacketFee) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.applications.fee.v1.MsgPayPacketFeeAsync";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.Fees.MessagePayPacketFeeAsync(
                this.PacketId.ToSerialization(),
                this.PacketFee.ToSerialization());
        }
    }
}
