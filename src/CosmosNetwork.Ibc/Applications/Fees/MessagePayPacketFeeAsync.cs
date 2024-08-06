using CosmosNetwork.Ibc.Core.Channel;

namespace CosmosNetwork.Ibc.Applications.Fees
{
    public record MessagePayPacketFeeAsync(
        PacketId PacketId,
        PacketFee PacketFee) : Message(COSMOS_DESCRIPTOR)
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
