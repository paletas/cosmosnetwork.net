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
                Fee.ToSerialization(),
                RefundAddress.Address,
                Relayers);
        }
    }
}
