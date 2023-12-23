using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Applications.Fees
{
    [ProtoContract]
    internal record PacketFee(
        [property: ProtoMember(1, Name = "fee")] Fee Fee,
        [property: ProtoMember(2, Name = "refund_address")] string RefundAddress,
        [property: ProtoMember(3, Name = "relayers")] string[] Relayers)
    {
        public Ibc.Applications.Fees.PacketFee ToModel()
        {
            return new Ibc.Applications.Fees.PacketFee(
                this.Fee.ToModel(),
                this.RefundAddress,
                this.Relayers);
        }
    }
}
