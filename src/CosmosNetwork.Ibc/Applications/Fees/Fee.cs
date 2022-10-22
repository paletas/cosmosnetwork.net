namespace CosmosNetwork.Ibc.Applications.Fees
{
    public record Fee(
        Coin[] ReceiverFee,
        Coin[] AcknowledgeFee,
        Coin[] TimeoutFee)
    {
        internal Serialization.Applications.Fees.Fee ToSerialization()
        {
            return new Serialization.Applications.Fees.Fee(
                ReceiverFee.Select(fee => fee.ToSerialization()).ToArray(),
                AcknowledgeFee.Select(fee => fee.ToSerialization()).ToArray(),
                TimeoutFee.Select(fee => fee.ToSerialization()).ToArray());
        }
    }
}
