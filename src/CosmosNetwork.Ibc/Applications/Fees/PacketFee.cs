namespace CosmosNetwork.Ibc.Applications.Fees
{
    public record PacketFee(
        Fee Fee,
        CosmosAddress RefundAddress,
        string[] Relayers)
    {
        internal Serialization.Applications.Fees.PacketFee ToSerialization()
        {
            return new Serialization.Applications.Fees.PacketFee(
                this.Fee.ToSerialization(),
                this.RefundAddress.Address,
                this.Relayers);
        }
    }
}
